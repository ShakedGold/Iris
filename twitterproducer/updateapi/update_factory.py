import re

from updatesproducer.updateapi.update import Update
from twitterproducer.updateapi.media_factory import MediaFactory
from twitterproducer.tweets.tweet import Tweet


class UpdateFactory:
    @staticmethod
    def to_update(tweet: Tweet):
        return Update(
            # Replace pic.twitter.com/232dssad links with nothing
            content=re.sub('pic.twitter.com/\\S+', '', tweet.text),
            author_id=tweet.username,
            creation_date=tweet.time,
            url=tweet.tweetUrl,
            media=MediaFactory.to_media(tweet)
        )
