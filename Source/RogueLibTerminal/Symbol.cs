namespace RogueLibTerminal
{
    public struct Symbol
    {
        public char Character;

        public Symbol(char character)
        {
            Character = character;
        }

        public Symbol(int character)
        {
            Character = (char) character;
        }

        public static implicit operator Symbol(char value)
        {
            return new Symbol(value);
        }

        public static implicit operator Symbol(int value)
        {
            return new Symbol(value);
        }
    }
}