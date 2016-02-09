﻿using UnityEngine;
using System.Collections;

public class PlayerAccesor : MonoBehaviour {

    // Player private fields
    private bool collideInEffect;
    private bool dashInEffect;
    private bool chargeInEffect;

    /////////////////////////////////////////////////////////////////

    /***** MISC. PLAYER VALUE GETTERS ******/
    // Game object Transform
    public Transform getTransform() {
        return this.transform;
    }

    // Player status Dashing
    public bool isDashing() {
        return dashInEffect;
    }

    // Player is in a collision?
    public bool isColliding() {
        return collideInEffect;
    }

    // Player is charging a dash?
    public bool isCharging() {
        return chargeInEffect;
    }


    /////////////////////////////////////////////////////////////////

    /*** MISC. PLAYER VALUE SETTERS ******/
    public void setDashing(bool b) {
        dashInEffect = b;
    }

    public void setCharging(bool b) {
        chargeInEffect = b;
    }

    public void setColliding(bool b) {
        collideInEffect = b;
    }

    /***** PUBLIC METHODS *****/
    // "Reset" a player's dash. i.e. Cancel the rest of the dash once a player hits another
    public void resetDash() {
        GetComponent<PlayerDash>().ResetDash();
        this.setDashing(false);
        this.setColliding(false);
    }
}
