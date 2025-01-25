using Godot;
using System;

public partial class AirFlowBuilder : Node
{
    [Export]
    public float AirFlowDistance { get; private set; } = 32 * 4;
    [Export]
    public PackedScene AirFlowSprite { get; private set; }

    public HairDryer HairDryerRef { get; private set; }

    public override void _Ready()
    {
        HairDryerRef = GetParent<HairDryer>();

        GetParent().Ready += LateReady;
    }

    private void LateReady()
    {
        var parent = GetParent<HairDryer>();
        float distance = GetDistanceWithWall();
        Vector2 startLocalPoint = parent.Ray.Position;

        InitAirFlowSprites(startLocalPoint, distance);
        InitAirFlowHitbox(startLocalPoint, distance);
    }

    private float GetDistanceWithWall()
    {
        using PhysicsRayQueryParameters2D query = new();
        query.CollisionMask = HairDryerRef.Ray.CollisionMask;
        query.From = HairDryerRef.Ray.GlobalPosition;
        query.To = HairDryerRef.Ray.ToGlobal(HairDryerRef.Ray.TargetPosition);

        var spaceRid = HairDryerRef.GetWorld2D().Space;
        using var space = PhysicsServer2D.SpaceGetDirectState(spaceRid);
        using var collider = space.IntersectRay(query);

        if (collider.Keys.Count == 0)
        {
            throw new Exception("Ray is not colliding with any wall");
        }

        var collisionPoint = collider["position"].As<Vector2>();
        float distance = HairDryerRef.Ray.GlobalPosition.DistanceTo(collisionPoint);

        return distance;
    }

    private void InitAirFlowSprites(Vector2 startLocalPoint, float distance)
    {
        uint nbSprite = ((uint)Math.Ceiling(distance / AirFlowDistance));
        for (uint spriteIdx = 0; spriteIdx < nbSprite; ++spriteIdx)
        {
            var spriteDistance = AirFlowDistance * spriteIdx;
            var spritePosition = HairDryerRef.Ray.Position + (Vector2.Down * spriteDistance);

            var sprite = AirFlowSprite.Instantiate<Node2D>();
            HairDryerRef.AddChild(sprite);
            sprite.Position = spritePosition;
        }
    }

    private void InitAirFlowHitbox(Vector2 startLocalPosition, float distance)
    {
        var area = HairDryerRef.EffectArea;
        var collision = area.GetChild(0) as CollisionShape2D;

        // Set area size
        using var shape = collision.Shape as RectangleShape2D;
        var newSize = shape.Size;
        newSize.Y = distance;
        shape.Size = newSize;

        // Set area position
        var newPosition = startLocalPosition + Vector2.Down * (distance / 2);
        var newGlobalPosition = HairDryerRef.ToGlobal(newPosition);
        collision.Position = area.ToLocal(newGlobalPosition);
    }
}
