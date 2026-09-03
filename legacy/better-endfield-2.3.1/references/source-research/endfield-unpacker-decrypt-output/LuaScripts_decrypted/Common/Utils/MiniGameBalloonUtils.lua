local MiniGameBalloonUtils = {}

MiniGameBalloonUtils.GAME_STATE = {
    FIRST_IN = "FirstIn",
    PLAY = "Play",
    SUCCESS = "Succ",
    COMPLETE = "Complete",
    END = "End",
}
MiniGameBalloonUtils.BalloonBlockState = CS.Beyond.Gameplay.BalloonBlockState

MiniGameBalloonUtils.BLOCK_STATE = {
    [1] = MiniGameBalloonUtils.BalloonBlockState.LIFT_1,
    [2] = MiniGameBalloonUtils.BalloonBlockState.LIFT_2,
    [3] = MiniGameBalloonUtils.BalloonBlockState.LIFT_3,
    [6] = MiniGameBalloonUtils.BalloonBlockState.LIFT_6,
    [0] = MiniGameBalloonUtils.BalloonBlockState.PLACE_ABLE,
    [-1] = MiniGameBalloonUtils.BalloonBlockState.CANT_PLACE,
}

MiniGameBalloonUtils.BLOCK_STATE_SORT = {
    [4] = MiniGameBalloonUtils.BalloonBlockState.LIFT_1,
    [3] = MiniGameBalloonUtils.BalloonBlockState.LIFT_2,
    [2] = MiniGameBalloonUtils.BalloonBlockState.LIFT_3,
    [1] = MiniGameBalloonUtils.BalloonBlockState.LIFT_6,
}

MiniGameBalloonUtils.FAKE_3D_BALLOON_MAT_PATH = "Assets/Beyond/DynamicAssets/Gameplay/UI/Materials/M_ui_fake_3d_balloon.mat"
MiniGameBalloonUtils.BALLOON_TYPE_TEX_PATH = "Assets/Beyond/DynamicAssets/Gameplay/UI/Textures/BalloonRecycle/icon_balloon_type_%d.png"

MiniGameBalloonUtils.BALLOON_TYPE_NAME = {
    [MiniGameBalloonUtils.BalloonBlockState.LIFT_1] = "LUA_BALLOON_LEVEL_1_NAME",
    [MiniGameBalloonUtils.BalloonBlockState.LIFT_2] = "LUA_BALLOON_LEVEL_2_NAME",
    [MiniGameBalloonUtils.BalloonBlockState.LIFT_3] = "LUA_BALLOON_LEVEL_3_NAME",
    [MiniGameBalloonUtils.BalloonBlockState.LIFT_6] = "LUA_BALLOON_LEVEL_4_NAME",
}


_G.MiniGameBalloonUtils = MiniGameBalloonUtils
return MiniGameBalloonUtils
