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

        public int ValueMove(Display.Board board, Move mv)
        {
            int value = 0;
            for (int i = 0; i < board.G.whitePieces.GetLength(0); i++)
            {
                if (board.G.whitePieces[i].Row == mv.toRow && board.G.whitePieces[i].Col == mv.toCol)
                {
                    // This means the move being made is a capture.
                    value = board.G.whitePieces[i].Value;
                }
            }
            return value;
        }

        // Calculates 1000 moves and takes the one with the highest value.
        // This is probably not effective for the future, but proves that it can prioritize piece taking for now.
        public void MoveCalculate(Display.Board board)
        {
            // We want to assign values to capturing pieces.
            // Alpha beta pruning? ...
            Move[] moves = new Move[1000];
            Piece p;
            Random rand = new Random();
            p = board.G.blackPieces[rand.Next(0, board.G.blackPieces.GetLength(0) - 1)];
            Move move = new Move();
            move.fromRow = p.Row;
            move.fromCol = p.Col;
            move.toRow = rand.Next(0, 8);
            move.toCol = rand.Next(0, 8);
            for (int i = 0; i < 1000; i++)
            {
                while (!p.ValidMove(move, board.G.whitePieces, board.G.blackPieces, p.Color))
                {
                    p = board.G.blackPieces[rand.Next(0, board.G.blackPieces.GetLength(0) - 1)];
                    move.fromRow = p.Row;
                    move.fromCol = p.Col;
                    move.toRow = rand.Next(0, 8);
                    move.toCol = rand.Next(0, 8);
                }
                moves[i] = move;
                p = board.G.blackPieces[rand.Next(0, board.G.blackPieces.GetLength(0) - 1)];
                move.fromRow = p.Row;
                move.fromCol = p.Col;
                move.toRow = rand.Next(0, 8);
                move.toCol = rand.Next(0, 8);

            }
            Move biggestMove = moves[0];
            for (int i = 1; i < moves.GetLength(0); i++)
            {
                if (ValueMove(board, moves[i]) > ValueMove(board, biggestMove))
                {
                    biggestMove = moves[i];
                }
            }
            Console.WriteLine(ValueMove(board, biggestMove));
            board.G.MakeMove(biggestMove, p);
            board.DrawBoxesFromDisplay(board.G.whitePieces, board.G.blackPieces);
            board.pbs[move.fromRow, move.fromCol].Image = null;
        }
    }
}