using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tanks.VL.ViewLayer.controller;
using Tanks.VL.ViewLayer.game_models;
using Tanks.VL.ViewLayer.view_models;

namespace Tanks.VL.ViewLayer
{
    public partial class GameStatsBoard : Form
    {
        PacmanController logic;

        List<CommonViewModel> data;
        public GameStatsBoard(PacmanController logik)
        {
            logic = logik;
            InitializeComponent();
            this.Location = new Point(400, 400);
        }

        public void UpdateModel()
        {
            data = new List<CommonViewModel>();
            data.Add(CommonViewModel.ToModel(logic.GetHeroModel())); 
            GetCommonViewModels(logic.GetEnemyModels());
            GetCommonViewModels(logic.GetBricksModels());
            GetCommonViewModels(logic.GetApplesModels());
            GetCommonViewModels(logic.GetBulletsModels());

            gameObjects.DataSource = data;
            gameObjects.Refresh();
        }

        private void GetCommonViewModels(List<BulletModel> models)
        {
            for (var i = 0; i < models.Count; i++)
            {
                data.Add(CommonViewModel.ToModel(models[i]));
            }
        }

        private void GetCommonViewModels(List<AppleModel> models)
        {
            for (var i = 0; i < models.Count; i++)
            {
                data.Add(CommonViewModel.ToModel(models[i]));
            }
        }

        private void GetCommonViewModels(List<BrickModel> models)
        {
            for (var i = 0; i < models.Count; i++)
            {
                data.Add(CommonViewModel.ToModel(models[i]));
            }
        }

        private void GetCommonViewModels(List<EnemyModel> models)
        {
            for (var i = 0; i < models.Count; i++)
            {
                data.Add(CommonViewModel.ToModel(models[i]));
            }
        }
    }
}
