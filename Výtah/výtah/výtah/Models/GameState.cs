using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using výtah.Models;

namespace Vytah.Models
{
    public class GameState : INotifyPropertyChanged
    {
        // Singleton
        private static GameState _instance;
        public static GameState Instance => _instance ??= new GameState();

        private GameState() { Reset(); }

        // --- Scéna ---
        private int _currentScene;
        public int CurrentScene
        {
            get => _currentScene;
            set { _currentScene = value; OnPropertyChanged(); }
        }

        // --- Inventář (max 3 itemy) ---
        public List<InventoryItem> Inventory { get; private set; } = new();

        public bool AddItem(InventoryItem item)
        {
            if (Inventory.Count >= 3) return false;
            Inventory.Add(item);
            OnPropertyChanged(nameof(Inventory));
            return true;
        }

        public bool RemoveItem(string itemId)
        {
            var item = Inventory.Find(i => i.Id == itemId);
            if (item == null) return false;
            Inventory.Remove(item);
            OnPropertyChanged(nameof(Inventory));
            return true;
        }

        public bool HasItem(string itemId) => Inventory.Exists(i => i.Id == itemId);

        // --- Flagy (co hráč udělal) ---
        public bool Scene1_EnteredLift { get; set; }
        public bool Scene2_FoundFuse { get; set; }
        public bool Scene2_PowerRestored { get; set; }
        public bool Scene2_FoundDrawing { get; set; }
        public bool Scene2_LadyDisappeared { get; set; }
        public bool Scene3_GotCard { get; set; }
        public bool Scene4_PowerRestored { get; set; }
        public bool Scene5_FinalChoice { get; set; }  // true = pustil muže ven
        public bool MuzhDied { get; set; }  // reveal: muž je mrtvý

        // --- Dialog ---
        private string _dialogText = "";
        public string DialogText
        {
            get => _dialogText;
            set { _dialogText = value; OnPropertyChanged(); }
        }

        private string _dialogSpeaker = "";
        public string DialogSpeaker
        {
            get => _dialogSpeaker;
            set { _dialogSpeaker = value; OnPropertyChanged(); }
        }

        private bool _dialogVisible;
        public bool DialogVisible
        {
            get => _dialogVisible;
            set { _dialogVisible = value; OnPropertyChanged(); }
        }

        // --- Reset ---
        public void Reset()
        {
            CurrentScene = 0;
            Inventory = new List<InventoryItem>();
            Scene1_EnteredLift = false;
            Scene2_FoundFuse = false;
            Scene2_PowerRestored = false;
            Scene2_FoundDrawing = false;
            Scene2_LadyDisappeared = false;
            Scene3_GotCard = false;
            Scene4_PowerRestored = false;
            Scene5_FinalChoice = false;
            MuzhDied = false;
            DialogText = "";
            DialogSpeaker = "";
            DialogVisible = false;
        }

        // --- INotifyPropertyChanged ---
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}