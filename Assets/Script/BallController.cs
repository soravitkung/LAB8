using UnityEngine;

public class BallController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float torqueAmount = 10f;
    public float magnusCoefficient = 2f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // 1. ทำให้บอลวิ่งไปข้างหน้าตั้งแต่เริ่ม เพื่อโชว์ Magnus Effect
        rb.linearVelocity = transform.forward * moveSpeed;
    }

    void Update()
    {
        // 2. แสดงผล Torque (ใช้ปุ่ม A) -> ค่าค่อยๆ เร่ง
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(Vector3.up * torqueAmount);
            Debug.Log("Using Torque: Angular Velocity is increasing...");
        }

        // 3. แสดงผล AngularVelocity โดยตรง (ใช้ปุ่ม D) -> ค่าเปลี่ยนทันที
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.angularVelocity = new Vector3(0, -15f, 0); // กำหนดค่าหมุนทันที
            Debug.Log("Directly set Angular Velocity!");
        }
    }

    void FixedUpdate()
    {
        // 4. คำนวณ Magnus Effect (ทำให้บอลเลี้ยวขณะวิ่ง)
        // สูตร: แรง Magnus = coefficient * (ความเร็วหมุน Cross กับ ความเร็วเคลื่อนที่)
        Vector3 magnusForce = Vector3.Cross(rb.angularVelocity, rb.linearVelocity) * magnusCoefficient;
        rb.AddForce(magnusForce);
    }
}