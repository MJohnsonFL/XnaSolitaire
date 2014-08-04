using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XnaSolitaire
{
    public class XnaSolitaire : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Background background = new Background();

        FaceDownZone DrawPile = new FaceDownZone(new Vector2(25, 50));
        FaceUpZone FaceUpPile = new FaceUpZone(new Vector2(200, 50));
        Hand MyHand = new Hand();

        List<PlayPile> PlayPiles = new List<PlayPile> { new PlayPile(new Vector2(25, 250)),
                                                        new PlayPile(new Vector2(200, 250)),
                                                        new PlayPile(new Vector2(375, 250)),
                                                        new PlayPile(new Vector2(550, 250)),
                                                        new PlayPile(new Vector2(725, 250)),
                                                        new PlayPile(new Vector2(900, 250)),
                                                        new PlayPile(new Vector2(1075, 250))};

        List<AcePile> AcePiles = new List<AcePile> { new AcePile(new Vector2(550, 50)),
                                                     new AcePile(new Vector2(725, 50)),
                                                     new AcePile(new Vector2(900, 50)),
                                                     new AcePile(new Vector2(1075, 50))};

        KeyboardState currentKey;
        KeyboardState previousKey;

        MouseState currentMouse;
        MouseState previousMouse;

        Vector2 mousePosition;
        Vector2 cardOffset;

        bool dragging = false;

        public XnaSolitaire()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Xna Solitaire";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 1024 + 256;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background.LoadContent(Content);

            DrawPile.LoadContent(Content);
            FaceUpPile.LoadContent(Content);

            foreach (AcePile acePile in AcePiles)
                acePile.LoadContent(Content);

            foreach (PlayPile playPile in PlayPiles)
                playPile.LoadContent(Content);

            SetupNewSolitaireGame();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            currentKey = Keyboard.GetState();
            currentMouse = Mouse.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (currentKey.IsKeyDown(Keys.B) && previousKey.IsKeyUp(Keys.B))
            {
                background.Change();
            }

            mousePosition = new Vector2(currentMouse.X, currentMouse.Y);
            Point mousePoint = new Point((int)mousePosition.X, (int)mousePosition.Y);

            // Mouse Click
            #region MouseClick where Previous == Released && Current == Pressed
            // On Mouse Click
            if (previousMouse.LeftButton == ButtonState.Released &&
                currentMouse.LeftButton == ButtonState.Pressed)
            {
                // Draw Pile is Empty so take all cards from Face Up Pile and Return them to Draw Pile
                if (DrawPile.IsEmpty && DrawPile.Rectangle.Contains(mousePoint))
                {
                    int cardCount = FaceUpPile.Cards.Count;

                    for (int i = 0; i < cardCount; i++)
                    {
                        DrawPile.AddCard(FaceUpPile.TakeCard());
                        DrawPile.Cards[i].isFaceDown = true;
                    }
                }
                // Draw a Card from Draw Pile and place in Face Up Pile
                if (!DrawPile.IsEmpty && DrawPile.TopCard.CardRectangle.Contains(mousePoint))
                {
                    Card c = DrawPile.TakeCard();
                    c.isFaceDown = false;
                    FaceUpPile.AddCard(c);
                }
                // Play Piles
                foreach (PlayPile playPile in PlayPiles)
                {
                    if (!playPile.IsEmpty && playPile.TopCard.isFaceDown)
                    {
                        playPile.TopCard.isFaceDown = false;
                    }
                }
            }
            #endregion

            // Dragging
            #region MouseClick where Current == Pressed && Previous == Pressed
            if (currentMouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Pressed)
            {
                if (dragging)
                {
                    MyHand.Update(mousePosition - cardOffset);
                }
                else if (FaceUpPile.TopCard.CardRectangle.Contains(mousePoint))
                {
                    cardOffset = mousePosition - FaceUpPile.TopCard.Position;
                    if (MyHand.Cards.Count < 1) // TEMP For Testing.........Remove Later
                        MyHand.AddFromDeck(FaceUpPile.TakeCard(), FaceUpPile);
                    dragging = true;
                }
                else if (PlayPiles[0].TopCard.CardRectangle.Contains(mousePoint))
                {
                    cardOffset = mousePosition - PlayPiles[0].TopCard.Position;
                    if (MyHand.Cards.Count < 1) // TEMP For Testing.........Remove Later
                        MyHand.AddFromDeck(PlayPiles[0].TakeCard(), PlayPiles[0]);
                    dragging = true;
                }
                else if (PlayPiles[1].TopCard.CardRectangle.Contains(mousePoint))
                {
                    cardOffset = mousePosition - PlayPiles[1].TopCard.Position;
                    if (MyHand.Cards.Count < 1) // TEMP For Testing.........Remove Later
                        MyHand.AddFromDeck(PlayPiles[1].TakeCard(), PlayPiles[1]);
                    dragging = true;
                }
                else if (PlayPiles[2].TopCard.CardRectangle.Contains(mousePoint))
                {
                    cardOffset = mousePosition - PlayPiles[2].TopCard.Position;
                    if (MyHand.Cards.Count < 1) // TEMP For Testing.........Remove Later
                        MyHand.AddFromDeck(PlayPiles[2].TakeCard(), PlayPiles[2]);
                    dragging = true;
                }
                else if (PlayPiles[3].TopCard.CardRectangle.Contains(mousePoint))
                {
                    cardOffset = mousePosition - PlayPiles[3].TopCard.Position;
                    if (MyHand.Cards.Count < 1) // TEMP For Testing.........Remove Later
                        MyHand.AddFromDeck(PlayPiles[3].TakeCard(), PlayPiles[3]);
                    dragging = true;
                }
                else if (PlayPiles[4].TopCard.CardRectangle.Contains(mousePoint))
                {
                    cardOffset = mousePosition - PlayPiles[4].TopCard.Position;
                    if (MyHand.Cards.Count < 1) // TEMP For Testing.........Remove Later
                        MyHand.AddFromDeck(PlayPiles[4].TakeCard(), PlayPiles[4]);
                    dragging = true;
                }
                else if (PlayPiles[5].TopCard.CardRectangle.Contains(mousePoint))
                {
                    cardOffset = mousePosition - PlayPiles[5].TopCard.Position;
                    if (MyHand.Cards.Count < 1) // TEMP For Testing.........Remove Later
                        MyHand.AddFromDeck(PlayPiles[5].TakeCard(), PlayPiles[5]);
                    dragging = true;
                }
                else if (PlayPiles[6].TopCard.CardRectangle.Contains(mousePoint))
                {
                    cardOffset = mousePosition - PlayPiles[6].TopCard.Position;
                    if (MyHand.Cards.Count < 1) // TEMP For Testing.........Remove Later
                        MyHand.AddFromDeck(PlayPiles[6].TakeCard(), PlayPiles[6]);
                    dragging = true;
                }
            }
            else
            {
                dragging = false;
            }
            #endregion

            // Mouse Released
            #region MouseClick where Previous == Pressed && Current == Released
            // Mouse Button Released
            if (previousMouse.LeftButton == ButtonState.Pressed &&
                currentMouse.LeftButton == ButtonState.Released)
            {
                if (MyHand.IsEmpty == false)
                {
                    Point cardCorner = new Point((int)MyHand.TopCard.Position.X, (int)MyHand.TopCard.Position.Y);

                    if (AcePiles[0].Rectangle.Contains(cardCorner))
                    {
                        if (AcePiles[0].IsLegalAdd(MyHand.TopCard))
                            AcePiles[0].AddCard(MyHand.TakeCard());
                        else
                            MyHand.LastDeck.AddCard(MyHand.TakeCard());
                    }
                    else if (AcePiles[1].Rectangle.Contains(cardCorner))
                    {
                        if (AcePiles[1].IsLegalAdd(MyHand.TopCard))
                            AcePiles[1].AddCard(MyHand.TakeCard());
                        else
                            MyHand.LastDeck.AddCard(MyHand.TakeCard());
                    }
                    else if (AcePiles[2].Rectangle.Contains(cardCorner))
                    {
                        if (AcePiles[2].IsLegalAdd(MyHand.TopCard))
                            AcePiles[2].AddCard(MyHand.TakeCard());
                        else
                            MyHand.LastDeck.AddCard(MyHand.TakeCard());
                    }
                    else if (AcePiles[3].Rectangle.Contains(cardCorner))
                    {
                        if (AcePiles[3].IsLegalAdd(MyHand.TopCard))
                            AcePiles[3].AddCard(MyHand.TakeCard());
                        else
                            MyHand.LastDeck.AddCard(MyHand.TakeCard());
                    }
                    else if (PlayPiles[0].Rectangle.Contains(cardCorner))
                    {
                        if (PlayPiles[0].IsLegalAdd(MyHand.TopCard))
                            PlayPiles[0].AddCard(MyHand.TakeCard());
                        else
                            MyHand.LastDeck.AddCard(MyHand.TakeCard());
                    }
                    else if (PlayPiles[1].Rectangle.Contains(cardCorner))
                    {
                        if (PlayPiles[1].IsLegalAdd(MyHand.TopCard))
                            PlayPiles[1].AddCard(MyHand.TakeCard());
                        else
                            MyHand.LastDeck.AddCard(MyHand.TakeCard());
                    }
                    else if (PlayPiles[2].Rectangle.Contains(cardCorner))
                    {
                        if (PlayPiles[2].IsLegalAdd(MyHand.TopCard))
                            PlayPiles[2].AddCard(MyHand.TakeCard());
                        else
                            MyHand.LastDeck.AddCard(MyHand.TakeCard());
                    }
                    else if (PlayPiles[3].Rectangle.Contains(cardCorner))
                    {
                        if (PlayPiles[3].IsLegalAdd(MyHand.TopCard))
                            PlayPiles[3].AddCard(MyHand.TakeCard());
                        else
                            MyHand.LastDeck.AddCard(MyHand.TakeCard());
                    }
                    else if (PlayPiles[4].Rectangle.Contains(cardCorner))
                    {
                        if (PlayPiles[4].IsLegalAdd(MyHand.TopCard))
                            PlayPiles[4].AddCard(MyHand.TakeCard());
                        else
                            MyHand.LastDeck.AddCard(MyHand.TakeCard());
                    }
                    else if (PlayPiles[5].Rectangle.Contains(cardCorner))
                    {
                        if (PlayPiles[5].IsLegalAdd(MyHand.TopCard))
                            PlayPiles[5].AddCard(MyHand.TakeCard());
                        else
                            MyHand.LastDeck.AddCard(MyHand.TakeCard());
                    }
                    else if (PlayPiles[6].Rectangle.Contains(cardCorner))
                    {
                        if (PlayPiles[6].IsLegalAdd(MyHand.TopCard))
                            PlayPiles[6].AddCard(MyHand.TakeCard());
                        else
                            MyHand.LastDeck.AddCard(MyHand.TakeCard());
                    }
                    else
                    {
                        MyHand.LastDeck.AddCard(MyHand.TakeCard());
                    }
                }
            }
            #endregion

            previousKey = currentKey;
            previousMouse = currentMouse;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            background.Draw(spriteBatch);

            DrawPile.Draw(spriteBatch);
            FaceUpPile.Draw(spriteBatch);

            foreach (AcePile acePile in AcePiles)
                acePile.Draw(spriteBatch);

            foreach (PlayPile playPile in PlayPiles)
                playPile.Draw(spriteBatch);

            MyHand.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void SetupNewSolitaireGame()
        {
            Deck deck = new Deck();

            deck.FillDeck();
            deck.Shuffle();

            foreach (Card card in deck.Cards)
            {
                card.LoadContent(Content);
            }

            //foreach (Card c in DrawPile.TakeCards(7))
            //{
            //    PlayPiles[6].AddCard(c);
            //}
            for (int i = 0; i < 7; i++)
            {
                Card c = deck.TakeCard();
                PlayPiles[6].AddCard(c);
            }
            for (int i = 0; i < 6; i++)
            {
                Card c = deck.TakeCard();
                PlayPiles[5].AddCard(c);
            }
            for (int i = 0; i < 5; i++)
            {
                Card c = deck.TakeCard();
                PlayPiles[4].AddCard(c);
            }
            for (int i = 0; i < 4; i++)
            {
                Card c = deck.TakeCard();
                PlayPiles[3].AddCard(c);
            }
            for (int i = 0; i < 3; i++)
            {
                Card c = deck.TakeCard();
                PlayPiles[2].AddCard(c);
            }
            for (int i = 0; i < 2; i++)
            {
                Card c = deck.TakeCard();
                PlayPiles[1].AddCard(c);
            }
            PlayPiles[0].AddCard(deck.TakeCard());

            PlayPiles[0].Cards[0].isFaceDown = false;
            PlayPiles[1].Cards[1].isFaceDown = false;
            PlayPiles[2].Cards[2].isFaceDown = false;
            PlayPiles[3].Cards[3].isFaceDown = false;
            PlayPiles[4].Cards[4].isFaceDown = false;
            PlayPiles[5].Cards[5].isFaceDown = false;
            PlayPiles[6].Cards[6].isFaceDown = false;

            for (int i = 0; i < deck.Cards.Count; i++)
            {
                Card c = deck.TakeCard();
                DrawPile.AddCard(c);
            }

        }
    }
}
