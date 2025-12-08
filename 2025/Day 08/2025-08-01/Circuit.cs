namespace _2025_08_01
{
    internal class Circuit
    {
        private static int _counter = 0;
        public List<JunctionBox> JunctionBoxes { get; }
        
        public int Id { get; }
        public Circuit(JunctionBox junctionBox)
        {
            Id = _counter++;
            JunctionBoxes = new List<JunctionBox>() { junctionBox };
        }

        public void MergeWith(Circuit circuit)
        {
            if (circuit.Id == Id)
            {
                return;
            }
            foreach (JunctionBox box in circuit.JunctionBoxes)
            {
                JunctionBoxes.Add(box);
                box.Circuit = this;
            }
        }
    }
}
