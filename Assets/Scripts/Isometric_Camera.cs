using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;

public class Isometric_Camera : MonoBehaviour
{
    // Start is called before the first frame update
        public Transform m_Target;
        public float m_Angle = 0f;
        public float m_Distance = 10f;
        public float m_Height = 7f;
        public float m_SmoothSpeed = 0.5f;
        private Vector3 refVelocity;

        void Start()
        {
            HandleCamera();
        }

        // Update is called once per frame
        void Update()
        {
            HandleCamera();
        }

        protected virtual void HandleCamera()
        {
            if (!m_Target)
            {
                return;
            }

            Vector3 worldPosition = (Vector3.forward * -m_Distance) + (Vector3.up * m_Height);
            //Debug.DrawLine(m_Target.position, worldPosition, Color.red);

            Vector3 rotatedVector = Quaternion.AngleAxis(m_Angle, Vector3.up) * worldPosition;
            //Debug.DrawLine(m_Target.position, rotatedVector, Color.blue);

            Vector3 flatTargetPosition = m_Target.position;
            flatTargetPosition.y = 0f;
            Vector3 finalPosition = flatTargetPosition + rotatedVector;
            //Debug.DrawLine(m_Target.position, finalPosition, Color.green);

            transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, m_SmoothSpeed);
            transform.LookAt(flatTargetPosition);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
            if (m_Target)
            {
                Gizmos.DrawLine(transform.position, m_Target.position);
                Gizmos.DrawSphere(m_Target.position, 1.5f);
            }
            Gizmos.DrawSphere(transform.position, 1.5f);
        }

}
