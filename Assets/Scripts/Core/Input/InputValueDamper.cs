using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HaewolWorkshop
{
    public abstract class InputValueDamper<T>
    {
        protected T current;
        protected T velocity;

        public abstract T getDampedValue(T target, float deltaTime);
    }


    public class InputVector2Damper : InputValueDamper<Vector2>
    {
        const float DampSpeed = 10;

        public override Vector2 getDampedValue(Vector2 target, float deltaTime)
        {
            current = Vector2.SmoothDamp(current, target, ref velocity, DampSpeed * deltaTime);

            return current;
        }
    }
}