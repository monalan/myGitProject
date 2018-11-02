from ext import db
from datetime import datetime

class PasteFile(db.model):
    __tablename__='PasteFile'
    id=db.Column(db.Integer,primary_key=True)
    filename = db.Column(db.String(5000),nullable=False)
    filehash = db.Column(db.String(128), nullable=False,unique = True)
    filemd5 = db.Column(db.String(128), nullable=False, unique=True)
    uploadtime = db.Column(db.DateTime,nullable=False)
    size = db.Column(db.Integer, nullable=False)

    def __init__(self,filename='',mimetype='application/octet-stream',size=0,filehash=None,filemd5=None):
        self.uploadtime = datetime.now()
        self.mimetype = mimetype
        self.size = int(size)
        self.filehash = filehash if filehash else  self._hash_filename(filename)
        self.filename = filename if filename else self.filehash
        self.filemd5 = filemd5

    @staticmethod
    def _hash_filename(filename):
        _, _, suffix