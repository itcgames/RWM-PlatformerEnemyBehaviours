using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class FollowerTest
    {
        private GameObject Player;
        private GameObject Enemy;

        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
            Player = GameObject.FindGameObjectWithTag("Player");
            Enemy = GameObject.FindGameObjectWithTag("Follower");
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(Player);
            Object.Destroy(Enemy);
            SceneManager.UnloadSceneAsync("TestScene");
        }

        [UnityTest]
        public IEnumerator StartPhaseTest()
        {
            float initialHealth = Enemy.GetComponent<FlyingFollower>().getHealth();
            Enemy.GetComponent<FlyingFollower>().invincible = true;
            Enemy.GetComponent<FlyingFollower>().damage(2);
            yield return new WaitForSeconds(0.1f);
            Assert.Equals(Enemy.GetComponent<FlyingFollower>().getHealth(), initialHealth);
        }

        [UnityTest]
        public IEnumerator StateChange()
        {
            bool initialstate = Enemy.GetComponent<FlyingFollower>().invincible;
            Player.GetComponent<Rigidbody2D>().position = new Vector2(Enemy.GetComponent<Rigidbody2D>().position.x - 2, Enemy.GetComponent<Rigidbody2D>().position.y - 10);
            yield return new WaitForSeconds(0.5f);
            Assert.AreNotEqual(Enemy.GetComponent<FlyingFollower>().invincible, initialstate);
        }

        [UnityTest]
        public IEnumerator TakeDamage()
        {
            float initialHealth = Enemy.GetComponent<FlyingFollower>().getHealth();
            Enemy.GetComponent<FlyingFollower>().invincible = false;
            Enemy.GetComponent<FlyingFollower>().damage(2);
            yield return new WaitForSeconds(0.1f);
            Assert.Less(Enemy.GetComponent<FlyingFollower>().getHealth(), initialHealth);
        }

        [UnityTest]
        public IEnumerator MoveLeft()
        {
            Vector2 initialPos = Enemy.GetComponent<Rigidbody2D>().position;
            Player.GetComponent<Rigidbody2D>().position = new Vector2(Enemy.GetComponent<Rigidbody2D>().position.x - 3, Enemy.GetComponent<Rigidbody2D>().position.y - 10);
            yield return new WaitForSeconds(5.0f);
            Assert.Less(Enemy.GetComponent<Rigidbody2D>().position.x, initialPos.x);
        }

        [UnityTest]
        public IEnumerator MoveRight()
        {
            Vector2 initialPos = Enemy.GetComponent<Rigidbody2D>().position;
            Player.GetComponent<Rigidbody2D>().position = new Vector2(Enemy.GetComponent<Rigidbody2D>().position.x + 3, Enemy.GetComponent<Rigidbody2D>().position.y - 10);
            yield return new WaitForSeconds(5.0f);
            Assert.Greater(Enemy.GetComponent<Rigidbody2D>().position.x, initialPos.x);
        }

        [UnityTest]
        public IEnumerator MoveDown()
        {
            Vector2 initialPos = Enemy.GetComponent<Rigidbody2D>().position;
            Player.GetComponent<Rigidbody2D>().position = new Vector2(Enemy.GetComponent<Rigidbody2D>().position.x - 3, Enemy.GetComponent<Rigidbody2D>().position.y - 10);
            yield return new WaitForSeconds(5.0f);
            Assert.Less(Enemy.GetComponent<Rigidbody2D>().position.y, initialPos.y);
        }

        [UnityTest]
        public IEnumerator MoveUp()
        {
            Vector2 initialPos = Enemy.GetComponent<Rigidbody2D>().position;
            Player.GetComponent<Rigidbody2D>().position = new Vector2(Enemy.GetComponent<Rigidbody2D>().position.x - 3, Enemy.GetComponent<Rigidbody2D>().position.y + 10);
            yield return new WaitForSeconds(5.0f);
            Assert.Greater(Enemy.GetComponent<Rigidbody2D>().position.y, initialPos.y);
        }
    }
}
