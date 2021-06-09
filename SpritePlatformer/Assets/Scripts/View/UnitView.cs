using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpritePlatformer
{
    public class UnitView : MonoBehaviour, IInteractive, IUnitView, IPool
    {
        public event Action<bool> evtTrigger = delegate { };
        public event Action<IInteractive, bool> evtCollision = delegate { };
        private List<Func<PackInteractiveData, int>> _evtAttack = new List<Func<PackInteractiveData, int>>();
        public event Action evtAnyCollision = delegate { };

        public Transform objectTransform => _objectTransform;
        private Transform _objectTransform;
        public Rigidbody2D objectRigidbody2D => _objectRigidbody2D;
        private Rigidbody2D _objectRigidbody2D;
        public SpriteRenderer objectSpriteRednderer => _objectSpriteRednderer;
        private SpriteRenderer _objectSpriteRednderer;


        [SerializeField] private TypeItem _typeItem;
        [SerializeField] private int _numCfg = 0;
        private PoolInstatiate _poolInstatiate;
        private bool _isPool = false;

        private Dictionary<int, int> _listCollisionEnter = new Dictionary<int, int>();

        event Func<PackInteractiveData, int> IInteractive.evtAttack
        {
            add
            {
                _evtAttack.Add(value);
            }

            remove
            {
                _evtAttack.Remove(value);
            }
        }

        public (TypeItem type, int cfg) GetTypeItem()
        {
            return (_typeItem, _numCfg);
        }

        public void SetTypeItem(TypeItem type = TypeItem.None, int cfg = -1)
        {
            if (cfg == -1) cfg = _numCfg;
            if (type == TypeItem.None) type = _typeItem;
            _typeItem = type; _numCfg = cfg;
        }

        private void Awake()
        {
            _objectTransform = transform;
            _objectRigidbody2D = GetComponent<Rigidbody2D>();
            if (_objectRigidbody2D==null) Debug.LogWarning($"does not find the Rigidbody2D on the {gameObject.name} object ");
            _objectSpriteRednderer = GetComponent<SpriteRenderer>();
            if (_objectSpriteRednderer == null) Debug.LogWarning($"does not find the SpriteRenderer on the {gameObject.name} object ");
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
                evtAnyCollision.Invoke();
                var ID = collision.gameObject.GetInstanceID();
                _listCollisionEnter[ID] = _listCollisionEnter.ContainsKey(ID) ? _listCollisionEnter[ID] + 1 : 1;

                if (_listCollisionEnter[ID] == 1 && collision.gameObject.TryGetComponent<IInteractive>(out IInteractive unitInteractive))
                {
                    evtCollision.Invoke(unitInteractive, true);
                }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {           
                evtTrigger.Invoke(true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
                evtTrigger.Invoke(false);
        }

        public int Attack(PackInteractiveData data)
        {
            int addScores = 0;
            foreach (var item in _evtAttack)
            {
                addScores += item(data);
            }
            return addScores;
        }

        public void SetPoolDestroy(PoolInstatiate poolInstatiate)
        {
            _poolInstatiate = poolInstatiate;
            _isPool = true;
        }

        void IInteractive.Kill()
        {
            if (_isPool) _poolInstatiate.DestroyGameObject(gameObject);
            else Destroy(gameObject);
        }
    }
}