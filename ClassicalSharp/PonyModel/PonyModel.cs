using System;
using ClassicalSharp;
using ClassicalSharp.Entities;
using ClassicalSharp.Model;
using ClassicalSharp.Physics;
using OpenTK;

namespace PonyPlugin {
	
	public class PonyModel : IModel {
		
		public PonyModel(Game window) : base(window) { }
		
		public override void CreateParts() {
			vertices = new ModelVertex[boxVertices * 17];
			RightWing2 = BuildBox(MakeBoxBounds(0, 0, 0, 13, 1, 8)
			                      .SetModelBounds(-17, 12.5f, -8, -4, 13.5f, 0)
			                      .TexOrigin(23, 39)
			                      .RotOrigin(-4, 13, -4));
			LeftWing2 = BuildBox(MakeBoxBounds(0, 0, 0, 13, 1, 8)
			                     .SetModelBounds(17, 12.5f, -8, 4, 13.5f, 0)
			                     .TexOrigin(23, 39)
			                     .RotOrigin(4, 13, -4));
			LeftWing = BuildBox(MakeBoxBounds(0, 0, 0, 1, 4, 7)
			                    .SetModelBounds(-5, 11, -5, -4, 15, 2)
			                    .TexOrigin(41, 17)
			                    .RotOrigin(-4, 18, -5));
			RightWing = BuildBox(MakeBoxBounds(0, 0, 0, 1, 4, 7)
			                     .SetModelBounds(5, 11, -5, 4, 15, 2)
			                     .TexOrigin(41, 17)
			                     .RotOrigin(-4, 18, -5));
			
			Head = BuildBox(MakeBoxBounds(0, 0, 0, 8, 8, 8)
			                .SetModelBounds(-4, 18, -10, 4, 26, -2)
			                .TexOrigin(0, 0)
			                .RotOrigin(0, 18, -5));
			Hat = BuildBox(MakeBoxBounds(0, 0, 0, 8, 8, 8)
			               .SetModelBounds(-4, 18, -10, 4, 26, -2)
			               .TexOrigin(32, 0)
			               .RotOrigin(0, 18, -5).Expand(0.5F));
			Snout = BuildBox(MakeBoxBounds(0, 0, 0, 3, 2, 1)
			                 .SetModelBounds(-1.5F, 18, -11, 1.5F, 20, -10)
			                 .TexOrigin(0, 37)
			                 .RotOrigin(0, 18, -5));
			Horn = BuildBox(MakeBoxBounds(0, 0, 0, 1, 5, 1)
			                .SetModelBounds(-0.5F, 26, -7, 0.5F, 31, -6)
			                .TexOrigin(24, 0)
			                .RotOrigin(0, 18, -5));
			LeftEar = BuildBox(MakeBoxBounds(0, 0, 0, 3, 4, 1)
			                   .SetModelBounds(-4.5F, 24, -5, -1.5F, 28, -4)
			                   .TexOrigin(0, 0)
			                   .RotOrigin(0, 18, -5));
			RightEar = BuildBox(MakeBoxBounds(0, 0, 0, 3, 4, 1)
			                    .SetModelBounds(4.5F, 24, -5, 1.5F, 28, -4)
			                    .TexOrigin(0, 0)
			                    .RotOrigin(0, 18, -5));
			Torso = BuildBox(MakeBoxBounds(0, 0, 0, 8, 8, 12)
			                 .SetModelBounds(-4, 8, -6, 4, 16, 6)
			                 .TexOrigin(0, 16)
			                 .RotOrigin(-4, 18, -5));
			Tail = BuildBox(MakeBoxBounds(0, 0, 0, 2, 16, 16)
			                .SetModelBounds(-2, 3, 6, 0, 19, 22)
			                .TexOrigin(41, 32)
			                .RotOrigin(-1, 14, 6));
			Neck = BuildBox(MakeBoxBounds(0, 0, 0, 4, 7, 5)
			                .SetModelBounds(-2, 12.5F, -7, 2, 19.5F, -2)
			                .TexOrigin(5, 36)
			                .RotOrigin(-4, 18, -5));
			
			LeftLegFront = BuildBox(MakeBoxBounds(0, 0, 0, 3, 8, 3)
			                        .SetModelBounds(0.5F, 0, -5.5F, 3.5F, 8, -2.5F)
			                        .TexOrigin(28, 16)
			                        .RotOrigin(-4, 8, -4));
			RightLegFront = BuildBox(MakeBoxBounds(0, 0, 0, 3, 8, 3)
			                         .SetModelBounds(-0.5F, 0, -5.5F, -3.5F, 8, -2.5F)
			                         .TexOrigin(28, 16)
			                         .RotOrigin(-4, 8, -4));
			LeftLegBack = BuildBox(MakeBoxBounds(0, 0, 0, 3, 8, 3)
			                       .SetModelBounds(1, 0, 4, 4, 8, 7)
			                       .TexOrigin(0, 16)
			                       .RotOrigin(-4, 8, 5));
			RightLegBack = BuildBox(MakeBoxBounds(0, 0, 0, 3, 8, 3)
			                        .SetModelBounds(-1, 0, 4, -4, 8, 7)
			                        .TexOrigin(0, 16)
			                        .RotOrigin(-4, 8, 5));
		}
		
		public override float NameYOffset { get { return 26/16f; } }
		
		public override float GetEyeY(Entity entity) { return 21/16f; }
		
		public override Vector3 CollisionSize {
			get { return new Vector3(8/16f + 0.6f/16f, 26.1f/16f, 8/16f + 0.6f/16f); }
		}
		
		public override AABB PickingBounds {
			get { return new AABB(-5/16f, 0, -14/16f, 5/16f, 16/16f, 9/16f); }
		}
		
		public override void DrawModel(Entity p) {
			vScale = 1 / 64f;
			game.Graphics.BindTexture(GetTexture(p));
			float headTilt = -p.HeadX;
			if(headTilt >= -180)
				headTilt *= 0.5f;
			else
				headTilt = -180 + headTilt * 0.5f; // -360 + (360 + headTilt) * 0.5f
			
			game.Graphics.AlphaTest = false;
			DrawRotate(headTilt * Utils.Deg2Rad, 0, 0, Head, true);
			DrawPart(Torso);
			UpdateVB();
			game.Graphics.AlphaTest = true;
			index = 0;
			
			if (NotAllBelowAir(p)) {
				DrawPart(LeftWing); //political
				DrawPart(RightWing); //political
			} else {
				const float legMax = 80 * Utils.Deg2Rad; // stolen from animatedcomponent
				float wingZRot = -(float)(Math.Cos(game.accumulator * 10) * legMax);
				DrawRotate(0, 0, -wingZRot, RightWing2, false);
				DrawRotate(0, 0, wingZRot, LeftWing2, false);
			}
			
			DrawRotate(headTilt * Utils.Deg2Rad, 0, 0, Hat, true);
			DrawRotate(headTilt * Utils.Deg2Rad -0.4F, 0, 0, Horn, true);
			DrawRotate(headTilt * Utils.Deg2Rad, 0, 0, LeftEar, true);
			DrawRotate(headTilt * Utils.Deg2Rad, 0, 0, RightEar, true);
			DrawRotate(headTilt * Utils.Deg2Rad, 0, 0, Snout, true);
			DrawRotate(-0.4F, 0, 0, Neck, false);
			const float tailTilt = 0.05F;
			const float tailRoll = 0.1F;
			DrawRotate(0, 0, 0, Tail, false);
			DrawRotate(0,  tailTilt, -tailRoll, Tail, false);
			DrawRotate(0, -tailTilt, tailRoll, Tail, false);
			
			if (!NotAllBelowAir(p)) {
				DrawPart(LeftLegFront);
				DrawPart(RightLegFront);
				DrawPart(LeftLegBack);
				DrawPart(RightLegBack);
			} else {
				DrawRotate(p.anim.leftLegX * 0.5F, 0, 0, LeftLegFront, false);
				DrawRotate(p.anim.rightLegX * 0.5F, 0, 0, RightLegFront, false);
				DrawRotate(p.anim.leftLegX * 0.5F, 0, 0, LeftLegBack, false);
				DrawRotate(p.anim.rightLegX * 0.5F, 0, 0, RightLegBack, false);
			}
			UpdateVB();
		}
		
		bool NotAllBelowAir(Entity p) {
			AABB bb = AABB.Make(p.Position, p.Size * 2);
			bb.Max.Y = p.Position.Y - 0.5f;
			bb.Min.Y = p.Position.Y - 1.5f;
			return p.TouchesAny(bb, (ushort b) => BlockInfo.Draw[b] != DrawType.Gas);
		}
		
		ModelPart Head, Horn, LeftEar, RightEar, Hat, Neck, Snout, Torso, LeftWing, RightWing, Tail, 
		LeftLegFront, RightLegFront, LeftLegBack, RightLegBack, LeftWing2, RightWing2;
	}
}
