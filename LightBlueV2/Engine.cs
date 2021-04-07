using System;

namespace LightBlueV2
{
    public class Engine 
    {
        public Engine()
        { }

        public void EngineMakeMove(Display.Board board)
        {
            // Let's just make a random move first.
            Piece p;
            Random rand = new Random();
            p = board.G.blackPieces[rand.Next(0, board.G.blackPieces.GetLength(0) - 1)];
            Move move = new Move();
            move.fromRow = p.Row;
            move.fromCol = p.Col;
            move.toRow = rand.Next(0,8);
            move.toCol = rand.Next(0, 8);
            while (!board.G.ValidateMove(move))
            {
                p = board.G.blackPieces[rand.Next(0, board.G.blackPieces.GetLength(0) - 1)];
                move.fromRow = p.Row;
                move.fromCol = p.Col;
                move.toRow = rand.Next(0, 8);
                move.toCol = rand.Next(0, 8);
            }
            board.G.MakeMove(move, p);
            
            board.DrawBoxesFromDisplay(board.G.whitePieces, board.G.blackPieces);
            board.pbs[move.fromRow, move.fromCol].Image = null;
        }
        public void EngineMoveOnePawn(Display.Board board)
        {
            Piece p;
            Random rand = new Random();
            p = board.G.blackPieces[8];
            Move move = new Move();
            move.fromRow = p.Row;
            move.fromCol = p.Col;
            move.toRow = p.Row + 2;
            move.toCol = p.Col;
            board.G.MakeMove(move, p);
            board.DrawBoxesFromDisplay(board.G.whitePieces, board.G.blackPieces);
        }
    }
}