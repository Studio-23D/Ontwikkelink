using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConnectionPointType { In, Out }

namespace NodeSystem
{
    public class ConnectionPoint : Element
    {
        private Node node;
        private Connection connection;
        private Type value;

        public void Init()
        {

        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}


