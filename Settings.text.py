def test_getSettings_returns_local_config(monkeypatch):
    monkeypatch.setattr(ConfigService, 'env', 'local')
    config_service = ConfigService()
    assert isinstance(config_service.getSettings(), LocalAppSettings)

def test_getSettings_returns_prod_config(monkeypatch):
    monkeypatch.setattr(ConfigService, 'env', 'prod')
    config_service = ConfigService()
    assert isinstance(config_service.getSettings(), ProdAppSettings)

def test_getSettings_returns_test_config(monkeypatch):
    monkeypatch.setattr(ConfigService, 'env', 'test')
    config_service = ConfigService()
    assert isinstance(config_service.getSettings(), TestAppSettings)

def test_getSettings_returns_dev_config(monkeypatch):
    monkeypatch.setattr(ConfigService, 'env', 'dev')
    config_service = ConfigService()
    assert isinstance(config_service.getSettings(), DevAppSettings)

def test_getSettings_returns_int_config(monkeypatch):
    monkeypatch.setattr(ConfigService, 'env', 'int')
    config_service = ConfigService()
    assert isinstance(config_service.getSettings(), IntegrationAppSettings)
