using UnityEngine;

public class BackgroundSize : MonoBehaviour
{
    void Start()
    {
        // 1. 위치를 카메라 정중앙(0, 0)으로 자동 이동
        transform.position = new Vector3(0f, 0f, transform.position.z);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null || sr.sprite == null) return;

        Camera cam = Camera.main;
        if (cam == null) return;

        // 2. 실제 카메라의 월드 세로/가로 크기 계산
        float worldScreenHeight = cam.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * cam.aspect;

        // 3. 스프라이트의 실제 원본 크기 계산 (PPU 반영)
        float spriteWidth = sr.sprite.rect.width / sr.sprite.pixelsPerUnit;
        float spriteHeight = sr.sprite.rect.height / sr.sprite.pixelsPerUnit;

        // 4. 화면에 빈 공간이 전혀 생기지 않도록 가로/세로 중 더 큰 비율로 확대
        float scaleX = worldScreenWidth / spriteWidth;
        float scaleY = worldScreenHeight / spriteHeight;
        float maxScale = Mathf.Max(scaleX, scaleY);

        // 5. 비율을 유지하며 스케일 적용
        transform.localScale = new Vector3(maxScale, maxScale, 1f);
    }
}