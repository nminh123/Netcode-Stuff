namespace NS.Utils
{
    public class Configuration
    {
        public struct Tag
        {
            public static string BULLET_SPAWN_POSITION_TAG { get; } = "bullet_spawn_position";
        }
        public struct NetworkMove
        {
            public static float SPEED { get; } = 5;
        }

        public struct NetworkShoot
        {
            public static float SPEED { get; } = 5;
            public static float BULLET_SPEED { get; } = 30f;
            public static float BULLET_LIFE_TIME { get; } = 5f;
            public static float FIRE_RATE { get; } = .25f;
        }

        public struct NetworkTopDownMove
        {
            public static float SPEED { get; } = 5f;
        }
    }
}