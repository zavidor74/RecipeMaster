using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RecipeMasterTool3
{
    using RecipeMaster1.Entities;

    /// <summary>
    /// Interaction logic for RecipeViewWindow.xaml
    /// </summary>
    public partial class RecipeViewWindow : Window
    {
        private readonly Recipe _r;

        public RecipeViewWindow(Recipe r)
        {
            _r = r;
            InitializeComponent();
            this.DataContext = r;
        }

        private void lstComponents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                var remoteRecipe = (item.Content as ComponentRef)?.RemoteEntity;
                if (remoteRecipe != null && remoteRecipe is Recipe rcp)
                {
                    var rcpWnd = new RecipeViewWindow(rcp);
                    rcpWnd.ShowDialog();
                }
            }
        }
    }
}
