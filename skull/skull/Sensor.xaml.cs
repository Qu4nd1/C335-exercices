using Microsoft.Maui.Devices.Sensors;

namespace skull
{
    public partial class Sensor : ContentPage
    {
        public Sensor()
        {
            InitializeComponent();

            // Vérifier si l'accéléromètre est disponible
            if (Accelerometer.Default.IsSupported)
            {
                // Définir la fréquence de mise à jour (peut être Low, Medium, High)
                Accelerometer.Default.ShakeDetected += Accelerometer_ShakeDetected;
                Accelerometer.Default.ReadingChanged += Accelerometer_ReadingChanged;

                // Démarrer la surveillance
                if (!Accelerometer.Default.IsMonitoring)
                {
                    Accelerometer.Default.Start(SensorSpeed.UI);
                }
            }
            else
            {
                lblStatus.Text = "L'accéléromètre n'est pas disponible sur cet appareil.";
            }
        }

        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            // Accéder aux valeurs de l'accéléromètre
            AccelerometerData data = e.Reading;

            // Mettre à jour l'interface utilisateur sur le thread UI
            MainThread.BeginInvokeOnMainThread(() =>
            {
                lblX.Text = $"X: {data.Acceleration.X:F2}";
                lblY.Text = $"Y: {data.Acceleration.Y:F2}";
                lblZ.Text = $"Z: {data.Acceleration.Z:F2}";
            });
        }

        private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                // Action à effectuer lors de la détection d'une secousse
                await DisplayAlert("Secousse détectée", "L'appareil a été secoué!", "OK");
            });
        }

        protected override void OnDisappearing()
        {
            if (Accelerometer.Default.IsSupported)
            {
                // Arrêter la surveillance et supprimer les gestionnaires d'événements
                Accelerometer.Default.Stop();
                Accelerometer.Default.ReadingChanged -= Accelerometer_ReadingChanged;
                Accelerometer.Default.ShakeDetected -= Accelerometer_ShakeDetected;
            }

            base.OnDisappearing();
        }
    }
}