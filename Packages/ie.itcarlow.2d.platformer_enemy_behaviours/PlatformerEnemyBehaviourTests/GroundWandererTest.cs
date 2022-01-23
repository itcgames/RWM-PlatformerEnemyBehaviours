using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class GroundWandererTest
    {
        private GameObject Enemy;
        private GameObject Wall;

        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
        }

        [TearDown]
        public void Teardown()
        {
            SceneManager.UnloadSceneAsync("TestScene");
        }

        [UnityTest]
        public IEnumerator MoveRight()
        {
            Enemy = GameObject.FindGameObjectWithTag("GroundWanderer");
            Vector2 initialPos = Enemy.GetComponent<Rigidbody2D>().position;
            yield return new WaitForSeconds(1.0f);
            Assert.Greater(Enemy.GetComponent<Rigidbody2D>().position.x, initialPos.x);
        }

        [UnityTest]
        public IEnumerator MoveLeft()
        {
            Enemy = GameObject.FindGameObjectWithTag("GroundWanderer");
            Enemy.GetComponent<GroundWanderer>().right = false;
            Vector2 initialPos = Enemy.GetComponent<Rigidbody2D>().position;
            yield return new WaitForSeconds(1.0f);
            Assert.Less(Enemy.GetComponent<Rigidbody2D>().position.x, initialPos.x);
        }

        [UnityTest]
        public IEnumerator EdgeRightTurn()
        {
            Enemy = GameObject.FindGameObjectWithTag("GroundWanderer");
            Enemy.GetComponent<Rigidbody2D>().position = new Vector2(18, Enemy.GetComponent<Rigidbody2D>().position.y);
            Vector2 initialPos = Enemy.GetComponent<Rigidbody2D>().position;
            yield return new WaitForSeconds(1.0f);
            Assert.Less(Enemy.GetComponent<Rigidbody2D>().position.x, initialPos.x);
        }

        [UnityTest]
        public IEnumerator EdgeLeftTurn()
        {
            Enemy = GameObject.FindGameObjectWithTag("GroundWanderer");
            Wall = GameObject.Find("Wall");
            Wall.gameObject.transform.position = new Vector3(18, Wall.gameObject.transform.position.y, Wall.gameObject.transform.position.z);
            Enemy.GetComponent<GroundWanderer>().right = false;
            Enemy.gameObject.transform.eulerAngles = new Vector3(0, -180, 0);
            Enemy.GetComponent<Rigidbody2D>().position = new Vector2(-18, Enemy.GetComponent<Rigidbody2D>().position.y);
            Vector2 initialPos = Enemy.GetComponent<Rigidbody2D>().position;
            yield return new WaitForSeconds(1.0f);
            Assert.Greater(Enemy.GetComponent<Rigidbody2D>().position.x, initialPos.x);
        }

        [UnityTest]
        public IEnumerator WallRightTurn()
        {
            Enemy = GameObject.FindGameObjectWithTag("GroundWanderer");
            Wall = GameObject.Find("Wall");
            Wall.gameObject.transform.position = new Vector3(18, Wall.gameObject.transform.position.y, Wall.gameObject.transform.position.z);
            Vector2 initialPos = Enemy.GetComponent<Rigidbody2D>().position;
            yield return new WaitForSeconds(1.5f);
            Assert.Less(Enemy.GetComponent<Rigidbody2D>().position.x, initialPos.x);
        }

        [UnityTest]
        public IEnumerator WallLeftTurn()
        {
            Enemy = GameObject.FindGameObjectWithTag("GroundWanderer");
            Wall = GameObject.Find("Wall");
            Enemy.GetComponent<GroundWanderer>().right = false;
            Enemy.gameObject.transform.eulerAngles = new Vector3(0, -180, 0);
            Enemy.GetComponent<Rigidbody2D>().position = new Vector2(-15, Enemy.GetComponent<Rigidbody2D>().position.y);
            Vector2 initialPos = Enemy.GetComponent<Rigidbody2D>().position;
            yield return new WaitForSeconds(1.5f);
            Assert.Greater(Enemy.GetComponent<Rigidbody2D>().position.x, initialPos.x);
        }

        [UnityTest]
        public IEnumerator TakeDamage()
        {
            Enemy = GameObject.FindGameObjectWithTag("GroundWanderer");
            float initialHealth = Enemy.GetComponent<GroundWanderer>().getHealth();
            Enemy.GetComponent<GroundWanderer>().damage(0.5f);
            yield return new WaitForSeconds(0.1f);
            Assert.Less(Enemy.GetComponent<GroundWanderer>().getHealth(), initialHealth);
        }

        [UnityTest]
        public IEnumerator SpeedUp()
        {
            Enemy = GameObject.FindGameObjectWithTag("GroundWanderer");
            Enemy.GetComponent<GroundWanderer>().speedBoost = true;
            float initialSpeed = Enemy.GetComponent<GroundWanderer>().speed;
            Enemy.GetComponent<GroundWanderer>().damage(0.5f);
            yield return new WaitForSeconds(0.1f);
            Assert.Greater(Enemy.GetComponent<GroundWanderer>().speed, initialSpeed);
        }
    }
}
