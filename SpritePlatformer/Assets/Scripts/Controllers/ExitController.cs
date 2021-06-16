using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpritePlatformer
{
    internal sealed class ExitController : IController,IExecute,IInitialization
    {
        private ControlLeak _controlLeak = new ControlLeak("ExitController");
        private GameM _gameM;
        private GameObject _gameObject;
        private Reference _reference;

        internal ExitController(GameM gameM, IInteractive iInteractive, GameObject gameObject,Reference reference)
        {
            iInteractive.evtAttack += Attack;
            _gameM = gameM;
            _gameObject = gameObject;
            _gameM.evtFlags += CompareFlags;
            _reference = reference;
        }

        
        private (int, bool) Attack(PackInteractiveData pack)
        {
            if (pack.typeItem == TypeItem.Player)
            {
                Congratulations();
                return (0, true);
            }
            return (0, false);
        }

        private void Congratulations()
        {
            Time.timeScale = 0;
            var prefab = LoadDataObjects.GetValue<GameObject>("Canvas/Congratulations");
            GameObject.Instantiate(prefab, _reference.Canvas);
            _gameM.isCongratulations = true;
        }

        private void CompareFlags()
        {
            if (_gameM.countFlags==0) _gameObject.SetActive(true);
        }

        public void Execute(float deltaTime)
        {
            if (_gameM.isCongratulations && Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        public void Initialization()
        {
            _gameObject.SetActive(false);
        }
    }
}