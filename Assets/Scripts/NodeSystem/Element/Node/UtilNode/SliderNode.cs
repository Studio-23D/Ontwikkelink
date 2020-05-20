using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{

    public abstract class SliderNode <T> : Node
    {
        protected string dropdownName;

        protected List<SliderOption<T>> sliderOptions = new List<SliderOption<T>>();

        protected T chosenValue;

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            base.Init(position, eventHandeler);

            chosenValue = sliderOptions[0].value;
        }

        public override void Draw()
        {

        }

        public void CreateOption(Texture2D _visual, T _value)
        {
            SliderOption<T> option = new SliderOption<T>
            {
                visual = _visual,
                value = _value,
            };
            sliderOptions.Add(option);
        }
    }
}
