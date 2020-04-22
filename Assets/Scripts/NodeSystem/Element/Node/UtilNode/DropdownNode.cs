using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
    public struct DropdownElement
    {
        public Texture2D visual;
        public object value;
    }

    public abstract class DropdownNode : Node
    {
        protected string dropdownName;

        protected List<DropdownElement> dropdownElements = new List<DropdownElement>();

        protected object chosenValue;

        private bool toggleDropdown = false;

        public override void Init(Vector2 position)
        {
            base.Init(position);

            chosenValue = dropdownElements[0].value;
        }

        public override void Draw()
        {
            base.Draw();

            if (GUI.Button(new Rect(0, 100, nodeRect.width, 20), dropdownName))
            {
                ToggleDropdown();
            }

            if (!toggleDropdown)
             return;

            Rect dropdrownRect = new Rect(nodeRect.width, 100, 200, 200);
            GUI.Box(dropdrownRect, "");
            for(int i = 0; i < dropdownElements.Count; i++)
            {
                if(GUI.Button(new Rect(nodeRect.width + (i % 2) * (nodeRect.width / 2), dropdrownRect.position.y + Mathf.FloorToInt(i / 2) * (dropdrownRect.height / 2), dropdrownRect.width / 2, dropdrownRect.height / 2), dropdownElements[i].visual))
                {
                    ToggleDropdown();
                    chosenValue = dropdownElements[i].value;
                }
            }
        }

        private void ToggleDropdown()
        {
            toggleDropdown = !toggleDropdown;
        }
    }
}
