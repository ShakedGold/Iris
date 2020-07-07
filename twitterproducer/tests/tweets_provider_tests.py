import logging
import unittest
from unittest import TestCase

from twitterproducer.tweets_provider import TweetsProvider


class TweetsProviderTests(TestCase):
    def __init__(self, *args, **kwargs):
        super(TweetsProviderTests, self).__init__(*args, **kwargs)

        logging.basicConfig(
            format='[%(asctime)s] %(message)s',
            datefmt='%Y-%m-%d %H:%M:%S %z',
            level=logging.DEBUG)

    def test1(self):
        user_id = '@Ayelet__Shaked'

        tweets = TweetsProvider(logging.getLogger(TweetsProvider.__name__)).get_updates(user_id)
        self.assertFalse(len(tweets) == 0)


if __name__ == '__main__':
    unittest.main()
