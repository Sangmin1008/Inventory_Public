# Inventory
Unity 기반의 C# 프로젝트로, 확장 가능한 이벤트 기반 아키텍처와 인벤토리 시스템, 사용자 인터페이스(UI) 시스템을 구현하고 있습니다. ScriptableObject와 싱글톤(Singleton) 패턴을 적극적으로 활용합니다.

# 개요
이 프로젝트는 Unity에서 게임 이벤트, 플레이어 입력, 인벤토리 동작, UI 상호작용을 모듈화된 시스템으로 처리합니다. 주요 구성 요소는 다음과 같습니다:

이벤트 시스템: ScriptableObject 기반의 커스텀 이벤트 채널로 느슨한 결합 구조 구현

입력 컨트롤러: 플레이어의 입력을 통합적으로 처리

인벤토리 시스템: 아이템 저장, 상호작용 및 툴팁 표시 기능

아이템 특성: ScriptableObject를 이용한 유연한 특성 정의

매니저: 게임 상태와 UI를 관리하는 핵심 매니저(GameManager, InventoryManager, StatusManager, UIManager)

UI 모듈: 인벤토리, 상태창, 메인 메뉴 및 슬롯 등을 위한 전용 UI 스크립트

유틸리티: Singleton<T> 기반 클래스 등 재사용 가능한 도구들
