/*
Jovonny Hailey
Final Project - Bejeweled
CSCI 3005
12/13/2018
 */
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

namespace JewelMatching
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameModel model;
        private GameView view;

        public MainWindow()
        {
            InitializeComponent();
            world.Focus();
            
            this.model = new GameModel(12, 10);
            this.view = new GameView(world, model);
            this.view.Update();
        }

        /// <summary>
        /// Mouse click function
        /// </summary>
        /// <param name="sender">Auto-gen parameter</param>
        /// <param name="e">Auto-gen parameter</param>
        private void world_MouseDown(object sender, MouseButtonEventArgs e)
        {
            model.RegisterClick((int)e.GetPosition(this).X, (int)e.GetPosition(this).Y);
            label.Text = "Score: " + model.Board.Score;
            this.view.Update();
        }
    }
}
