"""Add outhouses table

Revision ID: 9865cdf8de34
Revises: db36e8d2ddf6
Create Date: 2023-02-02 11:19:29.633541

"""
from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision = '9865cdf8de34'
down_revision = 'db36e8d2ddf6'
branch_labels = None
depends_on = None


def upgrade() -> None:
    op.create_table(
        "outhouses",
        sa.Column("id", sa.Integer(), nullable=False),
        sa.Column("name", sa.String(150), nullable=False),
        sa.PrimaryKeyConstraint("id"),
    )

    op.create_table(
        "user_outhouse_association_table",
        sa.Column("user_id", sa.Integer(), sa.ForeignKey("users.id")),
        sa.Column("outhouse_id", sa.Integer(), sa.ForeignKey("outhouses.id")),
        sa.PrimaryKeyConstraint("user_id", "outhouse_id"), 
    )

    op.drop_table("bookings")
    op.create_table(
        "bookings",
        sa.Column("id", sa.Integer(), nullable=False),
        sa.Column("startDate", sa.DateTime(), nullable=False),
        sa.Column("endDate", sa.DateTime(), nullable=False),
        sa.Column("userId", sa.Integer(), sa.ForeignKey("users.id"), nullable=False),
        sa.Column("outhouseId", sa.Integer(), sa.ForeignKey("outhouses.id"), nullable=False),
        sa.PrimaryKeyConstraint("id"),
    )


def downgrade() -> None:
    op.drop_table("outhouses")
    op.drop_table("user_outhouse_association_table")

