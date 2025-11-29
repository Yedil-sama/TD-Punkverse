using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace TD_Punkverse.Game.Enemies
{
    [RequireComponent(typeof(Canvas))]
    public class EnemyHealth : MonoBehaviour
    {
        private Canvas _enemyHealthCanvas;

        [SerializeField] private Image _healthIcon;

        private Enemy _enemy;
        public void Initialize(Enemy enemy)
        {
            _enemy = enemy;
            _enemyHealthCanvas.GetComponent<Canvas>();
            Subscribe();
        }

        private void Subscribe()
        {
           // _enemy.OnTakeDamage += HandleHealthChange;
        }

        private void Unsubscribe()
        {
           // _enemy.OnTakeDamage -= HandleHealthChange;
        }

        private void HandleHealthChange(Enemy enemy, int damageTaken)
        {
            
        }

        private void OnDestroy() => Unsubscribe();
        
    }
}
