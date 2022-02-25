using System;
using UnityEngine;

    [CreateAssetMenu(fileName = "NewPainting", menuName = "Painting")]
    public class Painting : ScriptableObject
    {
        [Header("General Infos")] 
        public bool _fake;
        public string _problem;
        public string _solution;

        [Header("Propetries")]
        public String _name;
        public String _autor;
        public String _date;
        public String _condition;
        [TextArea]
        public String _fact;
        
        [Header("Images")]
        public Sprite _painting;
        public Sprite _frame;
        public Sprite _damage;
        public Sprite _xray;
    }