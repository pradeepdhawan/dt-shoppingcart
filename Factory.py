import asyncio
from datetime import datetime
import cachetools

@pytest.mark.asyncio
async def test_create():
    loop = asyncio.get_event_loop()
    instance_name = "test_instance"
    solace_properties = {"host": "localhost", "username": "test", "password": "test"}
    cache_properties = {"host": "localhost", "username": "test", "password": "test"}
    solace_build_status_topic = "build_status"
    solace_price_status_topic = "price_status"
    pricing_request_queue = "pricing_requests"
    pricing_reply_topic = "pricing_replies"
    rejected_message_topic = "rejected_messages"
    model_type = "test_model"
    as_of_date = datetime.now()
    cache = cachetools.TTLCache(maxsize=100, ttl=300)
    use_risk_handler = True
    engine = ComputeEngineBuilder.create(instance_name, solace_properties, cache_properties, solace_build_status_topic, solace_price_status_topic, pricing_request_queue, pricing_reply_topic, rejected_message_topic, model_type, as_of_date, cache, use_risk_handler, loop)
    assert engine is not None
    assert isinstance(engine, ComputeEngine)
    assert engine.instance_name == instance_name
    assert engine.model_type == model_type
    assert engine.resolved_model_from_as_of_date == as_of_date
    assert engine.resolved_model_cache == cache
    assert isinstance(engine.solace_status_cache, SolaceStatusCache)
    assert isinstance(engine.pricing_request_receiver, QueueReceiver)
    assert isinstance(engine.pricing_reply_publisher, TopicPublisher)
    assert engine.pricing_reply_publisher_topic == pricing_reply_topic
    assert isinstance(engine.pricing_rejected_publisher, MessagePublisher)
    assert isinstance(engine.resolved_model, ResolvedModelController)
    assert isinstance(engine.result, ResultController)
    assert engine.use_risk_handler == use_risk_handler
    assert engine.loop == loop

