using RushingMachine.Entities.Interfaces;
using RushingMachine.Common.Pool;
using UnityEngine;

namespace RushingMachine.Entities.Cars.Weapon
{
    public class PlayerWeapon : IWeapon
    {
        private readonly ViewCache<string, MineView> _viewCache;
        private float _force = 5;
        private Transform _mineThrower;
        private MineView _mine;

        public PlayerWeapon(Transform mineThrower, MineView mine)
        {
            _viewCache = new ViewCache<string, MineView>(1);
            _mineThrower = mineThrower;
            _mine = mine;
        }

        public void Shoot()
        {
            //var mine = Object.Instantiate(_mine);
            const string id = "mine";
            var mineView = _viewCache.Create(id, _mine);
            mineView.Transform.position = _mineThrower.position;
            
            mineView.GetComponent<Rigidbody2D>().AddForce(_mineThrower.up * -1 * _force, ForceMode2D.Impulse);
            
            void Handler(MineView gameObject)
            {
                mineView.BecameInvisibleEvent -= Handler;
                DeactivateMine(id, gameObject);
            }

            mineView.BecameInvisibleEvent += Handler;
        }

        private void DeactivateMine(string id, MineView gameObject)
        {
            _viewCache.Destroy(id, gameObject);
        }
    }
}