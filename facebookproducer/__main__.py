import json
import logging

from facebookproducer.posts_provider import PostsProvider
from producer.mongodb_config import MongoDbConfig
from producer.producer import Producer
from producer.updates_repository import UpdatesRepository

from producer.topic_producer_config import TopicProducerConfig


def main():
    logging.basicConfig(
        format='[%(asctime)s] %(message)s',
        datefmt='%Y-%m-%d %H:%M:%S %z',
        level=logging.DEBUG)

    appsettings = json.load(open('appsettings.json'))

    repository = UpdatesRepository(
        MongoDbConfig(appsettings['mongodb']),
        logging.getLogger(UpdatesRepository.__name__)
    )

    posts_producer = Producer(
        TopicProducerConfig(appsettings['posts_producer']),
        repository,
        PostsProvider(),
        logging.getLogger(Producer.__name__))

    posts_producer.start()


if __name__ == '__main__':
    main()
