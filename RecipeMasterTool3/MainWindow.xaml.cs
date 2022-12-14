using System;
using System.Collections;
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
using RecipeMaster.Api;

namespace RecipeMasterTool3
{
    using RecipeMaster1.Entities;
    using RecipeMaster1.Entities.Transformations;

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

            JsonRecipeMasterServer jsonRecipeMasterServer =
                new JsonRecipeMasterServer("c:\\todelete\\recipemaster.json");
            
            RecipeDb recipeDb = new RecipeDb(jsonRecipeMasterServer);
            
            //var rcpWnd = new RecipeBrowserWindow(recipeDb);
            //rcpWnd.ShowDialog();
            var rcp = recipeDb.Recipes.Single(r => r.Name == "Lemon Tart");
            RecipeViewWindow rvw = new RecipeViewWindow(rcp, recipeDb);
            rvw.ShowDialog();
        }

        private void lbRecipes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                var rcpWnd = new RecipeBrowserWindow(null);
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

    public class RecipeDb
    {
        private readonly IRecipeMasterServer _recipeMasterServer;
        
        private List<Recipe> _recipes;
        
        private List<Flavor> _flavors;

        public List<Flavor> Flavors => _flavors;

        public RecipeDb(IRecipeMasterServer recipeMasterServer)
        {
            _recipeMasterServer = recipeMasterServer;
            _recipes = _recipeMasterServer.GetAllRecipes();
            _flavors = _recipeMasterServer.GetAllFlavors();
        }

        public IEnumerable<Recipe> Recipes
        {
            get
            {
                return _recipes;
            }
        }
    }

    public class JsonRecipeMasterServer : IRecipeMasterServer
    {
        public JsonRecipeMasterServer(string dbFile)
        {
            var db2 = RecipeMasterDatabase.RecipeMasterDatabase.LoadFrom("c:\\todelete\\recipemaster.json");
            RecipeMaster1.RecipeMaster.Instance.AttachDb(db2);
        }

        public List<Recipe> GetAllRecipes()
        {
            return RecipeMaster1.RecipeMaster.Instance.Recipes;
        }

        public List<Flavor> GetAllFlavors()
        {
            return RecipeMaster1.RecipeMaster.Instance.Flavors;
        }
    }
}
