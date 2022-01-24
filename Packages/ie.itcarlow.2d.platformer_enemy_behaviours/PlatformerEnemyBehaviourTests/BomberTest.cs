using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class BomberTest
    {
        private GameObject Player;
        private GameObject Enemy;
        private GameObject bomb;
        private GameObject[] shrapnel;

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
        public IEnumerator Movement()
        {

            Enemy = GameObject.FindGameObjectWithTag("Bomber");
            Vector2 initialPos = Enemy.GetComponent<Rigidbody2D>().position;
            yield return new WaitForSeconds(0.5f);
            Assert.Less(Enemy.GetComponent<Rigidbody2D>().position.x, initialPos.x);
        }

        [UnityTest]
        public IEnumerator BombSpawn()
        {

            Enemy = GameObject.FindGameObjectWithTag("Bomber");
            yield return new WaitForSeconds(0.5f);
            Assert.IsNotNull(Enemy.transform.GetChild(0));
        }

        [UnityTest]
        public IEnumerator BombCarry()
        {

            Enemy = GameObject.FindGameObjectWithTag("Bomber");
            Vector2 initialPos = Enemy.transform.GetChild(0).position;
            yield return new WaitForSeconds(0.5f);
            Assert.Less(Enemy.transform.GetChild(0).position.x, initialPos.x);
        }

        [UnityTest]
        public IEnumerator BombDrop()
        {
            Enemy = GameObject.FindGameObjectWithTag("Bomber");
            Vector2 initialPos = Enemy.transform.GetChild(0).position;
            yield return new WaitForSeconds(2.0f);
            Assert.Less(Enemy.transform.GetChild(0).position.y, initialPos.y);
        }

        [UnityTest]
        public IEnumerator BombExplosion()
        {

            Enemy = GameObject.FindGameObjectWithTag("Bomber");
            Enemy.GetComponent<Rigidbody2D>().position = new Vector2(5, 5);
            yield return new WaitForSeconds(1.5f);
            shrapnel = GameObject.FindGameObjectsWithTag("Shrapnel");
            Assert.IsNotNull(shrapnel);
        }

        [UnityTest]
        public IEnumerator BomberTakeDamage()
        {
            Enemy = GameObject.FindGameObjectWithTag("Bomber");
            float initialHealth = Enemy.GetComponent<Bomber>().getHealth();
            Enemy.GetComponent<Bomber>().Damage(2);
            yield return new WaitForSeconds(0.1f);
            Assert.Less(Enemy.GetComponent<Bomber>().getHealth(), initialHealth);
        }

        [UnityTest]
        public IEnumerator BombTakeDamage()
        {
            Enemy = GameObject.FindGameObjectWithTag("Bomber");
            float initialHealth = Enemy.GetComponentInChildren<Bomb>().getHealth();
            Enemy.GetComponentInChildren<Bomb>().Damage(0.5f);
            yield return new WaitForSeconds(0.1f);
            Assert.Less(Enemy.GetComponentInChildren<Bomb>().getHealth(), initialHealth);
        }
    }
}
