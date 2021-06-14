using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class FireController : IController, IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("FireController");
        private UnitM _unit;
        private Transform _positionFire;
        private ListControllers _listControllers;
        private FireCfg _fireCfg;
        private float fireTime;
        private Reference _reference;
        private PoolInstatiate _poolInstatiate;
        private GameObject _gameObject;

        internal FireController(UnitM unit, GameObject gameObject, FireCfg fireCfg, PoolInstatiate poolInstatiate, ListControllers listControllers, Reference reference)
        {
            _unit = unit;
            _positionFire = gameObject.transform.Find("positionFire");
            if (_positionFire == null) Debug.LogWarning($"Dont find positionFire at {gameObject.name}");
            _listControllers = listControllers;
            _fireCfg = fireCfg;
            _reference = reference;
            _unit.evtKill += Kill;
            _poolInstatiate = poolInstatiate;
            _gameObject = gameObject;
            fireTime = Time.time;
        }

        public void Execute(float deltaTime)
        {
            if (fireTime + _fireCfg.frequencyFire < Time.time)
            {
                fireTime = Time.time;
                var go = (GameObject)new CoreBuild(_poolInstatiate, _listControllers, _reference).CreateGameObjectPool(_reference.Trash).SetPosition(_positionFire.position, _positionFire.rotation).AddComponents();
                if (go.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                {                   
                    rb.AddForce(_gameObject.transform.right * _fireCfg.attackPower * rb.mass);
                }
                else Debug.LogWarning($"Dont get Rigidbody2d at {go.name}");
            }
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}