using UnityEngine;
using TMPro;

namespace SpritePlatformer
{
    internal sealed class ScoresCanvasController : IController
    {
        private ControlLeak _controlLeak = new ControlLeak("ScoresCanvasController");
        private UnitM _unitM;
        private TextMeshProUGUI text;

        internal ScoresCanvasController(UnitM unit)
        {
            _unitM = unit;

            var goTextScores = GameObject.FindObjectOfType<TagCanvasScores>();
            if (goTextScores==null) Debug.LogWarning($"Dont find the TagCanvasScores");
            text = goTextScores.GetComponent<TextMeshProUGUI>();
            _unitM.evtScores += UpdateScores;
            UpdateScores();
        }


        private void UpdateScores()
        {
            text.text=_unitM.scores.ToString();
        }

    }
}