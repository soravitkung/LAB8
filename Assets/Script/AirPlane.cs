using UnityEngine;

public class Airplane : MonoBehaviour
{
    [Header("Flight Physics")]
    [SerializeField] Rigidbody rb;
    public float enginePower = 100f;   // แรงผลัก (Thrust) [cite: 108, 115]
    public float liftBooster = 0.5f;  // แรงยก (Lift) [cite: 116, 130]
    public float drag = 0.01f;        // แรงต้านอากาศ (Linear Damping) [cite: 110, 117]
    public float angularDrag = 0.01f; // แรงต้านการหมุน (Angular Damping) [cite: 118]

    [Header("Controls")]
    public float pitchPower = 50f;    // แรงเชิดหัว [cite: 171, 175]

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // ตาม Lab หน้า 4 [cite: 102, 152]
        rb.interpolation = RigidbodyInterpolation.Interpolate; // เพื่อความนุ่มนวล [cite: 102, 156]
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // ป้องกันทะลุพื้น [cite: 103, 160]
    }

    void FixedUpdate()
    {
        // 1. Thrust: เร่งเครื่องไปข้างหน้าเมื่อกด Spacebar 
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.forward * enginePower);
        }

        // 2. คำนวณความเร็วไปข้างหน้า (Forward Speed) 
        float forwardSpeed = transform.InverseTransformDirection(rb.linearVelocity).z;

        // 3. Lift: แก้ปัญหาเครื่องลอยทันที (จะยกตัวเมื่อความเร็ว > 5 และเชิดหัวขึ้นเท่านั้น) [cite: 130, 170]
        if (forwardSpeed > 5f)
        {
            // คำนวณแรงยกสัมพันธ์กับความเร็ว 
            float lift = forwardSpeed * liftBooster;
            rb.AddRelativeForce(Vector3.up * lift);
        }

        // 4. Drag: แรงต้านอากาศตามที่กำหนดใน Lab [cite: 110, 131, 168]
        rb.linearDamping = drag;
        rb.angularDamping = angularDrag;

        // 5. Pitch: การเชิดหัว (ใช้ปุ่ม S เพื่อบินขึ้น) [cite: 171, 175, 182]
        float pitchInput = Input.GetAxis("Vertical");
         rb.AddRelativeTorque(Vector3.right * pitchInput * pitchPower); 
    }
}