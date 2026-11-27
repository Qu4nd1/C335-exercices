namespace skull;

public partial class Animation : ContentPage
{
    // Variables d'état
    private double _currentX = 0;
    private int _currentAngle = 0;
    private bool _isRotating = false;
    private int _speed = 10;
    public Animation()
	{
		InitializeComponent();

        // Démarrer la tâche de rotation en arrière-plan
        StartRotationTask();
    }

    // Gestionnaire d'événement pour le changement d'état du Switch
    private void OnRotationToggled(object sender, ToggledEventArgs e)
    {
        _isRotating = e.Value;
        rotationLabel.Text = e.Value ? "Rotation active" : "Rotation inactive";
    }

    // Gestionnaire d'événement pour le bouton de déplacement vers la gauche
    private async void OnMoveLeft(object sender, EventArgs e)
    {
        _currentX -= 50; // Déplacement de 50 pixels vers la gauche
        await box.TranslateTo(_currentX, 0, 250, Easing.SpringOut);
    }

    // Gestionnaire d'événement pour le bouton de déplacement vers la droite
    private async void OnMoveRight(object sender, EventArgs e)
    {
        _currentX += 50; // Déplacement de 50 pixels vers la droite
        await box.TranslateTo(_currentX, 0, 250, Easing.SpringOut);
    }

    // Gestionnaire d'événement pour le changement de valeur du Slider
    private void OnSpeedChanged(object sender, ValueChangedEventArgs e)
    {
        _speed = (int)e.NewValue;
        speedLabel.Text = $"Speed {_speed}";
    }

    // Tâche en arrière-plan pour gérer la rotation continue
    private async void StartRotationTask()
    {
        while (true)
        {
            if (_isRotating)
            {
                _currentAngle = (_currentAngle + _speed) % 360;
                await box.RotateTo(_currentAngle, 0); // Rotation sans animation (instantanée)
            }

            // Attendre pour contrôler la fréquence des mises à jour
            await Task.Delay(50);
        }
    }
}