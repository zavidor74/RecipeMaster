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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecipeMasterTool3
{
    using RecipeMaster1.Entities;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var db2 = RecipeMasterDatabase.RecipeMasterDatabase.LoadFrom("c:\\todelete\\recipemaster.json");
            RecipeMaster1.RecipeMaster.Instance.AttachDb(db2);
            lbRecipes.ItemsSource = db2.Recipes;
        }

        private void lbRecipes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                var rcpWnd = new RecipeWindow(item.Content as Recipe);
                rcpWnd.ShowDialog();
                /*
                var remoteRecipe = (item.Content as ComponentRef)?.RemoteEntity;
                if (remoteRecipe != null && remoteRecipe is Recipe rcp)
                {
                    var rcpWnd = new RecipeWindow(rcp);
                    var b = rcpWnd.ShowDialog();
                    var c = b;
                }*/
            }
        }
    }
}
