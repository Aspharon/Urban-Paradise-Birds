﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

/// <summary>
/// A class for helping out with input-related tasks, such as checking if a key or mouse button has been pressed.
/// </summary>
public class InputHelper
{
    // The current and previous mouse/keyboard states.
    MouseState currentMouseState, previousMouseState;
    KeyboardState currentKeyboardState, previousKeyboardState;
    GamePadState currentGamePadState, previousGamePadState;

    /// <summary>
    /// Updates the InputHelper object by retrieving the new mouse/keyboard state, and keeping the previous state as a back-up.
    /// </summary>
    /// <param name="gameTime">An object with information about the time that has passed in the game.</param>
    public void Update(GameTime gameTime)
    {
        // update the mouse and keyboard states
        previousMouseState = currentMouseState;
        previousKeyboardState = currentKeyboardState;
        previousGamePadState = currentGamePadState;
        currentMouseState = Mouse.GetState();
        currentKeyboardState = Keyboard.GetState();
        currentGamePadState = GamePad.GetState(PlayerIndex.One);
    }

    /// <summary>
    /// Gets the current position of the mouse cursor.
    /// </summary>
    public Vector2 MousePosition
    {
        get { return new Vector2(currentMouseState.X, currentMouseState.Y); }
    }

    /// <summary>
    /// Returns whether or not the left mouse button has just been pressed.
    /// </summary>
    /// <returns></returns>
    public bool MouseLeftButtonPressed()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
    }

    /// <summary>
    /// Returns whether or not a given keyboard key has just been pressed.
    /// </summary>
    /// <param name="k">The key to check.</param>
    /// <returns>true if the given key has just been pressed in this frame; false otherwise.</returns>
    public bool KeyPressed(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
    }

    /// <summary>
    /// Returns whether or not a given keyboard key is currently being held down.
    /// </summary>
    /// <param name="k">The key to check.</param>
    /// <returns>true if the given key is being held down; false otherwise.</returns>
    public bool KeyDown(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k);
    }

    /// <summary>
    /// Returns whether or not a given gamepad button is currently being held down.
    /// </summary>
    /// <param name="b">The button to check.</param>
    /// <returns>true if the given button is being held down; false otherwise.</returns>
    public bool ButtonPressed(Buttons b)
    {
        return currentGamePadState.IsButtonDown(b) && previousGamePadState.IsButtonUp(b);
    }

    /// <summary>
    /// Returns whether or not a given gamepad button is currently being held down.
    /// </summary>
    /// <param name="b">The button to check.</param>
    /// <returns>true if the given button is being held down; false otherwise.</returns>
    public bool ButtonDown(Buttons b)
    {
        return currentGamePadState.IsButtonDown(b);
    }
}