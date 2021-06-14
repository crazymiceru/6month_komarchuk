using UnityEngine;

namespace SpritePlatformer
{
    internal sealed class AimingController : IController,IExecute
    {
        private ControlLeak _controlLeak = new ControlLeak("AimingController");
        private UnitM _unit;
        private IUnitView _unitView;
        private ListControllers _listControllers;
        private Transform _target;
        FireCfg _fireCfg;

        internal AimingController(UnitM unit, IUnitView unitView, Transform target, FireCfg fireCfg, ListControllers listControllers)
        {
            _unit = unit;
            _unitView = unitView;
            _listControllers = listControllers;
            _unit.evtKill += Kill;
            _target = target;
            _fireCfg = fireCfg;
        }

        public void Execute(float deltaTime)
        {
            if (_target != null)
            {
                var direct = _target.position - _unitView.objectTransform.position;
                var targetRotate = Quaternion.LookRotation(Vector3.forward, direct) * Quaternion.Euler(0, 0, 90);


                //float z = targetRotate.eulerAngles.z;

                //if (z < _fireCfg.angleClamp.x || z > _fireCfg.angleClamp.y)
                //{
                //    var l1 = Mathf.Min(_fireCfg.angleClamp.x - z, 360-(_fireCfg.angleClamp.x - z));
                //    var l2 = Mathf.Min(_fireCfg.angleClamp.y - z, 360 - (_fireCfg.angleClamp.y - z));
                //    if (l1 < l2) z = _fireCfg.angleClamp.x; else z=_fireCfg.angleClamp.y;
                //}

                var z = targetRotate.eulerAngles.z.ClampAround(_fireCfg.angleClamp.x, _fireCfg.angleClamp.y);

                targetRotate.eulerAngles = new Vector3(targetRotate.eulerAngles.x, targetRotate.eulerAngles.y, z);
                
                _unitView.objectTransform.rotation = Quaternion.Lerp(_unitView.objectTransform.rotation, targetRotate, Time.deltaTime);
            }
        }

        void Kill()
        {
            _listControllers.Delete(this);
        }
    }
}