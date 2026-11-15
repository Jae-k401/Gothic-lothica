using UnityEngine;

public class AlignBottomLeft : MonoBehaviour
{
    [Header("화면 대비 세로 크기 비율 (0.755 = 화면 세로의 75.5%)")]
    [Range(0.1f, 1f)]
    public float heightRatio = 0.755f;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null || sr.sprite == null) return;

        Camera cam = Camera.main;
        if (cam == null) return;

        // 1. 카메라의 월드 기준 세로/가로 크기 계산
        float worldScreenHeight = cam.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * cam.aspect;

        // 2. 스프라이트의 원본 월드 크기 계산 (PPU 반영)
        float spriteWidth = sr.sprite.rect.width / sr.sprite.pixelsPerUnit;
        float spriteHeight = sr.sprite.rect.height / sr.sprite.pixelsPerUnit;

        // 3. 화면 세로 높이의 75.5%에 맞추어 동일 비율 스케일 적용
        float targetHeight = worldScreenHeight * heightRatio;
        float finalScale = targetHeight / spriteHeight;
        transform.localScale = new Vector3(finalScale, finalScale, 1f);

        // 4. 스케일 반영 후 실제 가로/세로 크기
        float finalSpriteWidth = spriteWidth * finalScale;
        float finalSpriteHeight = spriteHeight * finalScale;

        // 5. 카메라 좌하단 경계 계산
        float leftEdge = cam.transform.position.x - (worldScreenWidth / 2f);
        float bottomEdge = cam.transform.position.y - (worldScreenHeight / 2f);

        // 6. 이미지 중앙점이 기준이 되므로, 반값(Width/2, Height/2)만큼 이동해 좌하단 고정
        float targetX = leftEdge + (finalSpriteWidth / 2f);
        float targetY = bottomEdge + (finalSpriteHeight / 2f);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}