using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    // config params
    [SerializeField] private AudioClip _breakClip;
    [SerializeField] private GameObject collisionVFX;
    [SerializeField] Sprite[] hitSprites;

    // cached references
    private LevelManager _level;
    private SpriteRenderer _spriteRenderer;
    private int _maxHits;

    // state vars
    [SerializeField] int timesHit; // only serialized for debug purposes.

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _level = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.gameObject.CompareTag("Breakable"))
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        _maxHits = hitSprites.Length + 1;
        if (++timesHit >= _maxHits)
        {
            Destroy(this.gameObject);
            SendScoreChangeMessage();
            ApplyDestroyEffects();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            _spriteRenderer.sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from an array !" + "Gameobject name: " + gameObject.name);
        }
    }

    private void CountBreakableBlocks()
    {
        if (this.gameObject.CompareTag("Breakable"))
        {
            _level.CountBlocks();
        }
    }

    private void ApplyDestroyEffects()
    {
        AudioSource.PlayClipAtPoint(_breakClip, Camera.main.transform.position);
        TriggerVFX();
    }

    public void SendScoreChangeMessage()
    {
        _level.BlockDestroyed(_maxHits);
    }

    private void TriggerVFX()
    {
        var instance = Instantiate(collisionVFX, transform.position, transform.rotation);
        Destroy(instance, 1f);
    }
}
