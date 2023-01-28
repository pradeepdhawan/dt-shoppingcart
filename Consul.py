import json
import os
import pytest
from unittest.mock import patch
from pydantic import SecretStr

class TestConsul:
    @pytest.fixture
    def consul(self):
        return Consul(
            app_code="test_app",
            consul_url="http://test_consul_url",
            consul_token=None,
            consul_env="test_consul_env",
            consul_app_code="test_consul_app_code",
            environment="test_environment",
            region="test_region"
        )

    @patch('requests.get')
    def test_get_success(self, mock_requests_get, consul):
        # arrange
        test_key = "test_key"
        test_section = "test_section"
        test_response_value = '{"test_key": "test_value"}'
        test_response = {
            "Value": json.dumps(test_response_value).encode()
        }
        mock_requests_get.return_value.json.return_value = [test_response]
        # act
        result = consul.get(test_key, test_section)
        # assert
        assert result == json.loads(test_response_value)["test_key"]

    @patch('requests.get')
    def test_get_key_not_found(self, mock_requests_get, consul):
        # arrange
        test_key = "test_key"
        test_section = "test_section"
        mock_requests_get.return_value.json.return_value = []
        # act
        result = consul.get(test_key, test_section)
        # assert
        assert result is None

    @patch('requests.get')
    def test_get_request_exception(self, mock_requests_get, consul):
        # arrange
        test_key = "test_key"
        test_section = "test_section"
        mock_requests_get.side_effect = Exception("test exception")
        # act and assert
        with pytest.raises(Exception) as e:
            consul.get(test_key, test_section)
        assert str(e.value) == "test exception"

    @patch('requests.get')
    def test_get_decode_exception(self, mock_requests_get, consul):
        # arrange
        test_key = "test_key"
        test_section = "test_section"
        test_response_value = '{"test_key": "test_value"}'
        test_response = {
            "Value": json.dumps(test_response_value).encode()
        }
        mock_requests_get.return_value.json.return_value = [test_response]
        with patch.object(Consul, '_decode', side_effect=Exception("test exception")) as mock_decode:
            # act and assert
            with pytest.raises(Exception) as e:
                cons
