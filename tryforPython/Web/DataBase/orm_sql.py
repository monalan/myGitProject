from sqlalchemy import create_engine,Column,Integer,String,Sequence
from sqlalchemy.ext.declarative import  declarative_base
from sqlalchemy import and_,or_
from sqlalchemy.orm import sessionmaker
from sqlalchemy.sql import text
from consts import DB_URI

eng = create_engine(DB_URI)
Base = declarative_base()

class User(Base):
    __tablename__='users'
    id=Column(Integer,Sequence('user_id_seq'),primary_key=True,autoincrement=True)
    name = Column(String(50))

Base.metadata.drop_all(bind=eng)
Base.metadata.create_all(bind=eng)

Session = sessionmaker(bind=eng)
session = Session()

session.add_all([User(name=username) for username in ('xiaoming','wanglang','lilei')])
session.commit()

def get_result(rs):
    print('-'*20)
    for user in rs:
        print(user.name)

rs = session.query(User).all()
get_result(rs)
rs = session.query(User).filter(User.id.in_([2,]))
get_result(rs)
rs = session.query(User).filter(and_(User.id>2,User.id<4))
get_result(rs)
rs = session.query(User).filter(or_(User.id == 2,User.id == 4))
get_result(rs)
rs = session.query(User).filter(User.name.like('%min%'))
get_result(rs)
user = session.query(User).filter_by(name='xiaoming').first()
get_result([user])
rs = session.query(User).filter(text('id>2 and id<4')).order_by(text('id')).all()
get_result(rs)
rs = session.query(User).filter(text('id<:value and name=:name')).params(value=2,name='xiaoming').all()
get_result(rs)
rs = session.query(User).from_statement(text('SELECT * FROM users where name=:name')).params(name='wanglang').all()
get_result(rs)

