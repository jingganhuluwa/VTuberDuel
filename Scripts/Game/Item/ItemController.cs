using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class ItemController : Node2D
{
    public static ItemController Instance { get; private set; }

    public override void _EnterTree()
    {
        base._EnterTree();
        Instance = this;
    }

    private Vector2 _screenSize;
    private const int CollisionMaskItem = 1;
    private const int CollisionMaskItemSlot = 2;

    /// <summary>
    /// 当前拖动物体
    /// </summary>
    private Node2D _dragItem;


    //private bool _hoveringItem = false;

    /// <summary>
    /// 当前悬停物体
    /// </summary>
    private Node2D _hoveredItem;

    private Node2D _hoverItemSlot;

    public override void _Ready()
    {
        _screenSize = GetViewportRect().Size;
    }

    public override void _Process(double delta)
    {
        if (_dragItem != null)
        {
            Vector2 mousePosition = GetGlobalMousePosition();
            //设定拖动物体跟随鼠标位置,并限制屏幕空间
            _dragItem.Position = new Vector2(Mathf.Clamp(mousePosition.X, 20, _screenSize.X - 20),
                Mathf.Clamp(mousePosition.Y, 20, _screenSize.Y - 20));
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is not InputEventMouseButton eventMouseButton)
        {
            return;
        }

        if (eventMouseButton.ButtonIndex == MouseButton.Left)
        {
            if (eventMouseButton.Pressed)
            {
                //设定拖动的Item
                Node2D raycastCheckItem = RaycastCheckItem();
                if (raycastCheckItem != null)
                {
                    _dragItem = raycastCheckItem;
                    StartDragItem();
                }
            }
            else
            {
                FinishDragItem();
                _dragItem = null;
            }
        }
    }

    private void StartDragItem()
    {
        _dragItem.Scale = Vector2.One;
    }

    private void FinishDragItem()
    {
        if (_dragItem != null)
        {
            _dragItem.Scale = Vector2.One * 1.05f;
            
            //如果检测到卡槽,放置到卡槽
            if (RaycastCheckItemSlot())
            {
                _dragItem.Position = _hoverItemSlot.Position;
            }
        }
    }

    /// <summary> 射线检测获取点击的Item </summary>
    private Node2D RaycastCheckItem()
    {
        PhysicsDirectSpaceState2D spaceState = GetWorld2D().DirectSpaceState;
        var parameters = new PhysicsPointQueryParameters2D();
        parameters.Position = GetGlobalMousePosition();
        parameters.CollideWithAreas = true;
        parameters.CollisionMask = CollisionMaskItem;
        var result = spaceState.IntersectPoint(parameters);
        if (result.Count > 0)
        {
            //获取最高zIndex的物体
            //Area2D collider = (Area2D) result[0]["collider"];
            Node2D item = MaxZIndex(result);
            return item;
        }

        return null;
    }

    /// <summary> 射线检测获取点击的物品槽位 </summary>
    private bool RaycastCheckItemSlot()
    {
        PhysicsDirectSpaceState2D spaceState = GetWorld2D().DirectSpaceState;
        var parameters = new PhysicsPointQueryParameters2D();
        parameters.Position = GetGlobalMousePosition();
        parameters.CollideWithAreas = true;
        parameters.CollisionMask = CollisionMaskItemSlot;
        var result = spaceState.IntersectPoint(parameters);
        if (result.Count > 0)
        {
            //获取最高zIndex的物体
            //Area2D collider = (Area2D) result[0]["collider"];
            _hoverItemSlot = MaxZIndex(result);
            return true;
        }

        return false;
    }


    /// <summary>
    /// 获取最高zIndex的物体
    /// </summary>
    private Node2D MaxZIndex(Array<Dictionary> result)
    {
        List<Node2D> _raycastList = new List<Node2D>();
        foreach (Dictionary dict in result)
        {
            var area2D = (Area2D) dict["collider"];
            var item = area2D.GetParent() as Node2D;
            _raycastList.Add(item);
        }

        return _raycastList.MaxBy(item => item.ZIndex);
    }


    /// <summary>
    /// 悬停物体
    /// </summary>
    public void HoveredItem(Node2D hoveredItem)
    {
        if (_hoveredItem != null) return;
        HighLightItem(hoveredItem);
        _hoveredItem = hoveredItem;
    }

    /// <summary>
    /// 悬停离开
    /// </summary>
    public void HoveredOffItem(Node2D hoveredOffItem)
    {
        if (_hoveredItem == hoveredOffItem)
        {
            HighLightItem(hoveredOffItem, false);
            _hoveredItem = null;
            //悬停离开时,直接到另一悬停物体
            Node2D checkItem = RaycastCheckItem();
            if (checkItem != null)
            {
                HoveredItem(checkItem);
            }
        }
    }

    /// <summary>
    /// 高亮物体
    /// </summary>
    private void HighLightItem(Node2D item, bool highLight = true)
    {
        if (highLight)
        {
            item.Scale = Vector2.One * 1.05f;
            item.ZIndex = 2;
            return;
        }

        item.Scale = Vector2.One;
        item.ZIndex = 1;
    }
}