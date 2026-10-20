using System;
using System.Collections.Generic;
using BrakeToy.Core;

namespace BrakeToy.Tests
{
    // The same production C# and checks run in Unity and in the standalone .NET runner.
    public static class CoreChecks
    {
        public static Dictionary<string, Action> Create()
        {
            return new Dictionary<string, Action>
            {
                ["Touch begin does not teleport"] = () =>
                {
                    var s = NewSteering(); s.BeginDrag(); Near(s.Step(.1f), 0);
                },
                ["Relative input is resolution independent"] = () =>
                {
                    foreach (float width in new[] { 360f, 720, 1080, 1440, 2160 })
                    {
                        var s = NewSteering(); s.BeginDrag(); s.Drag(width * .2f, width);
                        Near(s.Step(1), 1.8f);
                    }
                },
                ["Lift immediately holds current position"] = () =>
                {
                    var s = NewSteering(); s.BeginDrag(); s.Drag(900, 1080); s.Step(.01f);
                    float held = s.Position; s.EndDrag(); Near(s.Step(5), held);
                },
                ["Retouch does not jump or carry old target"] = () =>
                {
                    var s = NewSteering(); s.BeginDrag(); s.Drag(200, 1080); s.Step(.03f);
                    s.EndDrag(); float held = s.Position; s.BeginDrag(); Near(s.Step(1), held);
                    s.Drag(-108, 1080); Near(s.Step(1), held - .9f);
                },
                ["Movement stays inside both boundaries"] = () =>
                {
                    var s = NewSteering(); s.BeginDrag(); s.Drag(100000, 360); Near(s.Step(10), 3.11f);
                    s.Drag(-100000, 360); Near(s.Step(10), -3.11f);
                },
                ["Direction reverses immediately at boundary"] = () =>
                {
                    var s = NewSteering(); s.BeginDrag(); s.Drag(10000, 360); s.Step(1);
                    s.Drag(-36, 360); Near(s.Step(1), 2.21f);
                },
                ["Maximum lateral speed is enforced"] = () =>
                {
                    var s = NewSteering(); s.BeginDrag(); s.Drag(300, 360); Near(s.Step(.02f), .19f);
                },
                ["Input without a drag and zero width are ignored"] = () =>
                {
                    var s = NewSteering(); s.Drag(500, 360); Near(s.Step(1), 0);
                    s.BeginDrag(); s.Drag(500, 0); Near(s.Step(1), 0);
                },
                ["Restart resets all steering state"] = () =>
                {
                    var s = NewSteering(); s.BeginDrag(); s.Drag(108, 1080); s.Step(1); s.Reset();
                    Near(s.Position, 0); Near(s.Target, 0); Require(!s.IsDragging);
                },
                ["30 60 and 120 FPS reach the same target"] = () =>
                {
                    foreach (int fps in new[] { 30, 60, 120 })
                    {
                        var s = NewSteering(); s.BeginDrag();
                        for (int i = 0; i < fps; i++) { s.Drag(108f / fps, 1080); s.Step(1f / fps); }
                        Near(s.Position, .9f);
                    }
                },
                ["Swept collision catches forward tunneling"] = () =>
                {
                    Require(SweptCollision.TryHit(0, -20, 0, 20, 1, 1, out float t)); Near(t, .475f);
                },
                ["Swept collision catches lateral tunneling"] = () =>
                {
                    Require(SweptCollision.TryHit(-3, 0, 3, 0, .5f, 1, out float t)); Near(t, 2.5f / 6);
                },
                ["Diagonal swept collision catches both moving axes"] = () =>
                {
                    Require(SweptCollision.TryHit(-3, -3, 3, 3, 1, 1, out float t)); Near(t, 1f / 3);
                },
                ["A separated path does not collide"] = () =>
                {
                    Require(!SweptCollision.TryHit(2, -10, 2, 10, 1, 1, out _));
                },
                ["Crossing each axis at different times does not collide"] = () =>
                {
                    Require(!SweptCollision.TryHit(-3, -8, 3, 0, .5f, .5f, out _));
                },
                ["Initial overlap and contact count as collision"] = () =>
                {
                    Require(SweptCollision.TryHit(0, 0, 0, 0, 1, 1, out float t)); Near(t, 0);
                    Require(SweptCollision.TryHit(1, -2, 1, 2, 1, 1, out _));
                },
                ["Obstacle behind the player moving away does not collide"] = () =>
                {
                    Require(!SweptCollision.TryHit(0, 3, 0, 10, 1, 1, out _));
                },
                ["Stop at contact cancels pending steering"] = () =>
                {
                    var s = NewSteering(); s.BeginDrag(); s.Drag(200, 1080); s.Step(.1f);
                    s.StopAt(.4f); Near(s.Step(10), .4f); Require(!s.IsDragging);
                },
                ["10000 seeded sweeps match independent closest approach oracle"] = () =>
                {
                    var random = new Random(4719);
                    for (int i = 0; i < 10000; i++)
                    {
                        float x0 = Next(random), z0 = Next(random), x1 = Next(random), z1 = Next(random);
                        float hx = .2f + (float)random.NextDouble() * 2, hz = .2f + (float)random.NextDouble() * 2;
                        bool hit = SweptCollision.TryHit(x0, z0, x1, z1, hx, hz, out float t);
                        bool expected = SegmentIntersectsBox(x0, z0, x1, z1, hx, hz);
                        Require(hit == expected, "Seed 4719 sweep " + i);
                        if (hit)
                        {
                            Require(t >= 0 && t <= 1);
                            Require(Math.Abs(x0 + (x1 - x0) * t) <= hx + .0001f);
                            Require(Math.Abs(z0 + (z1 - z0) * t) <= hz + .0001f);
                        }
                    }
                }
            };
        }

        private static SteeringModel NewSteering() => new SteeringModel(3.11f, 9, 9.5f);
        private static float Next(Random r) => (float)(r.NextDouble() * 20 - 10);
        private static void Require(bool condition, string message = "Expected true")
        { if (!condition) throw new Exception(message); }
        private static void Near(float actual, float expected)
        { Require(Math.Abs(actual - expected) < .0001f, "Expected " + expected + ", got " + actual); }

        // Independent geometry oracle: segment endpoints plus intersections with each box edge.
        private static bool SegmentIntersectsBox(double x0, double z0, double x1, double z1, double hx, double hz)
        {
            if (Math.Abs(x0) <= hx && Math.Abs(z0) <= hz || Math.Abs(x1) <= hx && Math.Abs(z1) <= hz) return true;
            foreach (double edge in new[] { -hx, hx })
            {
                if (x1 == x0) continue;
                double t = (edge - x0) / (x1 - x0), z = z0 + t * (z1 - z0);
                if (t >= 0 && t <= 1 && Math.Abs(z) <= hz) return true;
            }
            foreach (double edge in new[] { -hz, hz })
            {
                if (z1 == z0) continue;
                double t = (edge - z0) / (z1 - z0), x = x0 + t * (x1 - x0);
                if (t >= 0 && t <= 1 && Math.Abs(x) <= hx) return true;
            }
            return false;
        }
    }
}
