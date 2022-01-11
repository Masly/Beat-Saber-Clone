using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * For now, Video settings will take care of anything related to both aestethic and actual settings, since there is no need to get more complex than this at the moment
 */
public interface IVideoSettings
{
    Material leftMaterial
    {
        get;
    }

    Material rightMaterial
    {
        get;
    }
}