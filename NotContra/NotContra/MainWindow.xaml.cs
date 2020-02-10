using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace NotContra
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Hero hero;
        private GameModel model;
        private Terrain terrain;
        private GameView view;
        private DispatcherTimer gameTimer;

        public MainWindow()
        {
            InitializeComponent();
            world.Focus();
            BuildNewGame();

            view.Update();

            gameTimer = CreateTimer(25, true, OnUpdateView);
        }

        private void BuildNewGame()
        {
            BuildTerrain();

            hero = new Hero(this.terrain.GetStart());
            model = new GameModel(terrain, hero);
            view = new GameView(world, model);
        }


        DispatcherTimer CreateTimer(int milliseconds,
                                    bool enabled,
                                    EventHandler callbackMethod)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, milliseconds);
            timer.IsEnabled = enabled;
            timer.Tick += callbackMethod;
            return timer;
        }

        void OnUpdateView(object sender, EventArgs e)
        {
            view.Update();
            model.Update();

            if(model.ShouldRestart())
            {
                BuildNewGame();
            }
        }

        private void BuildTerrain()
        {
            MemoryStream stream = new MemoryStream(
                Encoding.UTF8.GetBytes(LevelResources.level001)
                );

            TerrainBuilder builder = new TerrainBuilder(stream);

            builder.Build();

            terrain = builder.Terrain;
        }

        private void world_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                case Key.Left:
                case Key.NumPad4:
                    hero.Left();
                    break;
                case Key.D:
                case Key.Right:
                case Key.NumPad6:
                    hero.Right();
                    break;
                case Key.W:
                case Key.Up:
                case Key.NumPad8:
                case Key.LeftAlt:
                case Key.RightAlt:
                    hero.Jump();
                    break;
                case Key.Space:
                    hero.Shoot();
                    break;
            }
        }

        private void world_KeyUp(object sender, KeyEventArgs e)
        {
            this.hero.StopRunning();
        }
    }
}
