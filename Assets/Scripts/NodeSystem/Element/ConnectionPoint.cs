using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum ConnectionPointType { In, Out }

namespace NodeSystem
{
    public class ConnectionPoint : Element
    {
        private Node node;
        private Connection connection;
        private FieldInfo value;

        public ConnectionPoint(FieldInfo value)
        {
            this.value = value;
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


