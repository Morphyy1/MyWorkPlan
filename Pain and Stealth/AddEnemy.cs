using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pain_and_Stealth
{
    public class AddEnemy
    {
        public List<AnimationEnimies> SetPositionEnemy(List<AnimationEnimies> enemySpawn)
        {
            enemySpawn = new List<AnimationEnimies>
            {
                new AnimationEnimies { X = 2700},
                new AnimationEnimies { X = 2800},
                new AnimationEnimies { X = 2900},

                new AnimationEnimies { X = 5050, Healthy = 5},
                new AnimationEnimies { X = 5150, Healthy = 5},
                new AnimationEnimies { X = 5250, Healthy = 5},
                new AnimationEnimies { X = 5350, Healthy = 5},
                new AnimationEnimies { X = 5450, Healthy = 5},
                new AnimationEnimies { X = 5550, Healthy = 5},


                new AnimationEnimies { X = 6650, Healthy = 6},
                new AnimationEnimies { X = 6750, Healthy = 6},
                new AnimationEnimies { X = 6850, Healthy = 6},
                new AnimationEnimies { X = 6950, Healthy = 6},
                new AnimationEnimies { X = 7050, Healthy = 6},
                new AnimationEnimies { X = 7150, Healthy = 6},
                new AnimationEnimies { X = 7250, Healthy = 6},
                new AnimationEnimies { X = 7350, Healthy = 6},
                new AnimationEnimies { X = 7450, Healthy = 6},
                new AnimationEnimies { X = 7550, Healthy = 6}
            };
            return enemySpawn;
        }
    }
}
