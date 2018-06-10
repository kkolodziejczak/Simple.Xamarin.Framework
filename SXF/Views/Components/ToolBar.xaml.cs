using System;
using System.Collections;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SXF
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToolBar : ContentView
    {
        public ToolBar()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
              propertyName: "ItemsSource",
              returnType: typeof(IEnumerable),
              declaringType: typeof(ToolBar),
              defaultValue: null,
              propertyChanged: ItemsChanged
            );

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void ItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            IEnumerable oldValueAsEnumerable;
            IEnumerable newValueAsEnumerable;
            try
            {
                oldValueAsEnumerable = oldValue as IEnumerable;
                newValueAsEnumerable = newValue as IEnumerable;
            }
            catch (Exception)
            {
                throw;
            }

            var control = (ToolBar)bindable;
            if (oldValue is INotifyCollectionChanged oldObservableCollection)
            {
                oldObservableCollection.CollectionChanged -= control.OnItemsSourceCollectionChanged;
            }

            if (newValue is INotifyCollectionChanged newObservableCollection)
            {
                newObservableCollection.CollectionChanged += control.OnItemsSourceCollectionChanged;
            }

            control.MainGrid.Children.Clear();
            control.MainGrid.ColumnDefinitions.Clear();

            var counter = 0;
            foreach (var item in newValueAsEnumerable)
            {
                control.MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                var view = control.CreateChildViewFor(item);
                control.MainGrid.Children.Add(view, counter, 0);
                counter++;
            }

            control.UpdateChildrenLayout();
            control.InvalidateLayout();
        }

        private View CreateChildViewFor(object item)
        {
            if (ItemTemplate == null)
                return null;
            ItemTemplate.SetValue(BindingContextProperty, item);
            return (View)ItemTemplate.CreateContent();
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var invalidate = false;

            if (e.OldItems != null)
            {
                this.MainGrid.Children.RemoveAt(e.OldStartingIndex);
                invalidate = true;
            }

            if (e.NewItems != null)
            {
                for (var i = 0; i < e.NewItems.Count; ++i)
                {
                    var item = e.NewItems[i];
                    var view = this.CreateChildViewFor(item);

                    this.MainGrid.Children.Insert(i + e.NewStartingIndex, view);
                }

                invalidate = true;
            }

            if (invalidate)
            {
                this.UpdateChildrenLayout();
                this.InvalidateLayout();
            }
        }

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
              propertyName: "ItemTemplate",
              returnType: typeof(DataTemplate),
              declaringType: typeof(ToolBar),
              defaultValue: default(DataTemplate),
              propertyChanged: OnChange
            );

        private static void OnChange(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ToolBar)bindable;
            ItemsTemplate = newValue as DataTemplate;
            control.ItemTemplate = newValue as DataTemplate;
        }

        public static DataTemplate ItemsTemplate;

        public DataTemplate ItemTemplate
        {
            get
            {
                return ItemsTemplate;
                return (DataTemplate)GetValue(ItemTemplateProperty);
            }
            set { SetValue(ItemTemplateProperty, value); }
        }
    }
}