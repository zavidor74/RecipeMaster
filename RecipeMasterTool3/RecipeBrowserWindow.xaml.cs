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
    /// <summary>
    /// Interaction logic for RecipeBrowserWindow.xaml
    /// </summary>
    public partial class RecipeBrowserWindow : Window
    {
        public RecipeBrowserWindow(RecipeDb recipeDb)
        {
            InitializeComponent();
            lstRecipes.ItemsSource = recipeDb.Recipes;
        }
    }
}
